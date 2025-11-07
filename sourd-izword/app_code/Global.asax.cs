using FluentScheduler;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

public class Global : HttpApplication
{
    private static System.Threading.Thread keepAliveThread = new System.Threading.Thread(KeepAlive);
    private static IRavenClient _client;

    protected void Application_Start()
    {
        _client = new RavenClient("https://b4acf76ef4b84253a41742e83e1b5a59@o1299550.ingest.sentry.io/4503927489036288");
        DataServiceFactory.Init();
        var db = DataServiceFactory.GetDbService();

        var dataSet = db.PP.QueryMultiple(@"SELECT * FROM pp_lang;SELECT * FROM pp_config;SELECT * FROM pp_page ORDER BY PathPattern DESC;SELECT * FROM pp_json");
        var langs = dataSet.Read<PP_Lang>().ToList();
        var configs = dataSet.Read<PP_Config>().ToList();
        var pages = dataSet.Read<PP_Page>().ToList();
        var jsons = dataSet.Read<PP_Json>().ToList();

        Root.ReloadLangs(langs);
        Root.RefreshConfigs(configs);
        MyRouteTable.RefreshRoutes(pages);
        Root.RefreshCategoryIndexes();

        jsons.ForEach(json =>
        {
            if (json.JsonKey == nameof(VisitCounter.UrlStats))
            {
                var urls = Json.Decode<Dictionary<string, int>>(json.JsonContent);
                foreach (var key in urls.Keys)
                {
                    VisitCounter.UrlStats.AddOrUpdate(key, urls[key], (k, c) => urls[key] + c);
                }
            }
        });

        if (!HttpContext.Current.IsDebuggingEnabled) keepAliveThread.Start();

        JobManager.Initialize();
        JobManager.JobException += (exceptionInfo) => HandleEvent(exceptionInfo.Exception, false);
        JobManager.AddJob(() => VisitCounter.RefreshVisitStats(), s => s.NonReentrant().ToRunEvery(15).Minutes());
        //JobManager.AddJob(() => VisitCounter.RefreshVisitStats(), s => s.NonReentrant().ToRunEvery(1).Days().At(0, 0));
    }

    protected void Application_End(object sender, EventArgs e)
    {
        var db = DataServiceFactory.GetDbService();
        db.Update(new PP_Json { JsonKey = nameof(VisitCounter.UrlStats), JsonContent = Json.Encode(VisitCounter.UrlStats) });
    }

    private static void KeepAlive()
    {
        string website = ConfigurationManager.AppSettings["website"];

        while (true)
        {
            try
            {
                var req = System.Net.WebRequest.Create(website);
                req.GetResponse();
                System.Threading.Thread.Sleep(60000);
            }
            catch (System.Threading.ThreadAbortException)
            {
                break;
            }
        }
    }

    protected void Application_Error(object sender, EventArgs e)
    {
        HandleEvent(Server.GetLastError(), false);
    }

    protected void Session_Start(object sender, EventArgs e)
    {
        try
        {
            VisitCounter.OnSessionStart_New();
        }
        catch (Exception ex)
        {
            HandleEvent(ex, false);
        };
    }

    protected void Application_AcquireRequestState(object sender, EventArgs e)
    {
        try
        {
            VisitCounter.OnRequestBegin_New();
        }
        catch (Exception ex)
        {
            HandleEvent(ex, false);
        };
    }

    protected void Session_End(object sender, EventArgs e)
    {
        try
        {
            VisitCounter.OnSessionEnd_New(this.Session);
        }
        catch (Exception ex)
        {
            HandleEvent(ex, false);
        };
    }

    private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        HandleEvent(e.Exception, true);
    }

    private void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {
        HandleEvent(null, true);
    }

    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        HandleEvent(e.Exception as Exception, !e.Observed);
    }

    [HandleProcessCorruptedStateExceptions]
    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        HandleEvent(e.ExceptionObject as Exception, e.IsTerminating);
    }

    private void HandleEvent(System.Exception exception, bool runtimeEnding)
    {
        if (exception == null) return;
        _client.Capture(new SentryEvent(exception));
    }
}