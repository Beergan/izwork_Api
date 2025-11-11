using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ChatHub : Hub
{
	private IDataService Db = DataServiceFactory.GetDbService();
	private string Room(string domain, string branchGuid)
		=> $"{domain?.Trim().ToLower()}:{branchGuid?.Trim().ToLower()}";

	public override Task OnConnected()
	{
		var domain = Context.QueryString["domain"];
		var branchGuid = Context.QueryString["branchGuid"];
		var role = Context.QueryString["role"] ?? "customer";

		if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(branchGuid))
			throw new HubException("Missing params");

		Groups.Add(Context.ConnectionId, Room(domain, branchGuid));
		Clients.Group(Room(domain, branchGuid)).presenceChanged(new
		{
			type = "join",
			role,
			connectionId = Context.ConnectionId,
			at = DateTime.UtcNow.ToString("o")
		});
		return base.OnConnected();
	}
	public override Task OnDisconnected(bool stopCalled)
	{
		var domain = Context.QueryString["domain"];
		var branchGuid = Context.QueryString["branchGuid"];
		Groups.Remove(Context.ConnectionId, Room(domain, branchGuid));

		Clients.Group(Room(domain, branchGuid)).presenceChanged(new
		{
			type = "leave",
			connectionId = Context.ConnectionId,
			at = DateTime.UtcNow.ToString("o")
		});
		return base.OnDisconnected(stopCalled);
	}

	public void JoinGroup(string domain, string branchGuid)
	{
		Groups.Add(Context.ConnectionId, Room(domain, branchGuid));
		System.Diagnostics.Debug.WriteLine($"Client {Context.ConnectionId} join thêm group: {Room(domain, branchGuid)}");
	}

	public void SendCustomerMessage(string domain, string branchGuid, string branchName, string content, string customerGuid, string avatarUrl, string replyGuid, string repContent, string staffName, string repTo)
	{
		try
		{
			if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(branchGuid))
				throw new Exception("Thiếu domain hoặc branchGuid");

			var msg = new MessageJson
			{
				Guid = Guid.NewGuid().ToString("N"),
				Role = "user",
				StaffGuid = customerGuid,
				AvatarUrl = avatarUrl,
				Content = content,
				StaffName = staffName,
				TimeSent = DateTime.Now.ToString(),
				ReplyGuid = replyGuid == "0" ? "0" : replyGuid,
				RepContent = repContent,
				RepTo = repTo
			};

			System.Diagnostics.Debug.WriteLine($"SendCustomerMessage {domain}/{branchGuid}: {content}");

			SaveMessage(branchGuid, msg);

			Clients.Group(Room(domain, branchGuid)).newMessage(new
			{
				role = "user",
				userGuid = customerGuid,
				branchGuid,
				branchName,
				message = msg
			});
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.WriteLine($"Lỗi trong SendCustomerMessage: {ex}");
			throw;
		}
	}

	public void SendStaffMessage(string domain, string branchGuid, string content, string staffGuid, string staffName, string avatarUrl, string replyGuid, string repContent, string repUser)
	{
		var msg = new MessageJson
		{
			Guid = Guid.NewGuid().ToString("N"),
			Role = "admin",
			StaffGuid = staffGuid,
			StaffName = staffName,
			AvatarUrl = avatarUrl,
			Content = content,
			TimeSent = DateTime.UtcNow.ToString("o"),
			ReplyGuid = replyGuid == "0" ? "0" : replyGuid,
			RepContent = repContent,
			RepTo = repUser
		};

		SaveMessage(branchGuid, msg);
		Clients.Group(Room(domain, branchGuid)).newMessage(new { role = "admin", staffName, branchGuid, message = msg });
	}

	private void SaveMessage(string branchGuid, MessageJson msg)
	{
		try
		{
			var branch = Db.Table<pp_branch>()
				.Where(b => b.BranchGuid == branchGuid)
				.FirstOrDefault();

			if (branch == null)
			{
				System.Diagnostics.Debug.WriteLine($"Không tìm thấy branch {branchGuid}");
				return;
			}

			var list = branch.Messages ?? new List<MessageJson>();
			list.Add(msg);

			if (list.Count > 1000000)
				list = list.Skip(list.Count - 1000000).ToList();

			branch.Messages = list;
			Db.Update(branch);

			System.Diagnostics.Debug.WriteLine($"Lưu {list.Count} tin nhắn vào branch {branchGuid}");
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.WriteLine($"Lỗi SaveMessage: {ex}");
			throw;
		}
	}
}
