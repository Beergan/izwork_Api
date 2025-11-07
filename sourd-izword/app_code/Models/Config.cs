using SLK.Common;

public class Config
{
    public Config()
    {
        MainMenus = new MenuItem[]
        {
            new MenuItem {
                Title ="TRANG CHỦ",
                Href ="#"
            },
            new MenuItem {
                Title ="GIỚI THIỆU",
                Href ="#"
            },                    
            new MenuItem {
                Title ="CÔNG TRÌNH",
                Href ="#",
                SubMenus =  new MenuItem[]{ new MenuItem { Title = "item con 1", Href = "#" }, new MenuItem { Title = "item con 2", Href = "#" }, new MenuItem { Title = "item con 2", Href = "#" } }
            },
            new MenuItem {
                Title ="DỊCH VỤ",
                Href ="#",
                SubMenus =  new MenuItem[]{ new MenuItem { Title = "item con 1", Href = "#" }, new MenuItem { Title = "item con 2", Href = "#" }, new MenuItem { Title = "item con 2", Href = "#" } }
            },
            new MenuItem {
                Title ="VẬT LIỆU XÂY DỰNG",
                Href ="#",
                SubMenus =  new MenuItem[]{ new MenuItem { Title = "item con 1", Href = "#" }, new MenuItem { Title = "item con 2", Href = "#" }, new MenuItem { Title = "item con 2", Href = "#" } }
            },
            new MenuItem {
                Title ="PHONG THỦY",
                Href ="#",
                SubMenus =  new MenuItem[]{ new MenuItem { Title = "item con 1", Href = "#" }, new MenuItem { Title = "item con 2", Href = "#" }, new MenuItem { Title = "item con 2", Href = "#" } }
            },
            new MenuItem {
                Title ="TIN TỨC",
                Href ="#",
                SubMenus =  new MenuItem[]{ new MenuItem { Title = "item con 1", Href = "#" }, new MenuItem { Title = "item con 2", Href = "#" }, new MenuItem { Title = "item con 2", Href = "#" } }
            },
            new MenuItem {
                Title ="HỘP THƯ",
                Href ="#"                    
            },
            new MenuItem {
                Title ="LIÊN HỆ",
                Href ="#"
            },
        };
        this.WebTitle = "VIET GROUP";        
        this.Favicon = "/assets/images/pre-logo.png";
        this.GoogleGtag = "1234";
        this.LinkFacebook = "https://facebook.com";
        this.LinkInstagram = "https://www.instagram.com";
        this.LinkTwitter = "#";
        this.Company = new CompanyInfo
        {
                          
        };        
    }        

    [Field(
        Title = "Tiêu đề website",
        Required = false,
        Control = InputControlType.TextBox)]
    public string WebTitle { get; set; }

    [Field(
        Title = "Tên website",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Description { get; set; }

    [Field(
        Title = "Logo",
        Required = false,
        Control = InputControlType.Image)]
    public string Logo { get; set; }

    [Field(
        Title = "Loader",
        Required = false,
        Control = InputControlType.Image)]
    public string Loader { get; set; }

    [Field(
        Title = "Favicon",
        Required = false,
        Control = InputControlType.Image)]
    public string Favicon { get; set; }

    [Field(
        Title = "Ảnh chia sẻ",
        Required = false,
        Control = InputControlType.Image)]
    public string ImageShare { get; set; }

    [Field(
        Title = "Google Gtag",
        Required = false,
        Control = InputControlType.TextBox)]
    public string GoogleGtag { get; set; }    

    [Field(
       Title = "Link facebook",
       Required = false,
       Control = InputControlType.TextBox)]
    public string LinkFacebook { get; set; }

    [Field(
      Title = "Link twitter",
      Required = false,
      Control = InputControlType.TextBox)]
    public string LinkTwitter { get; set; }

    [Field(
      Title = "Link linkedin",
      Required = false,
      Control = InputControlType.TextBox)]
    public string Linkedin { get; set; }

    [Field(
      Title = "Link google",
      Required = false,
      Control = InputControlType.TextBox)]
    public string LinkGoogle { get; set; }

    [Field(
      Title = "Link youtube",
      Required = false,
      Control = InputControlType.TextBox)]
    public string LinkYoutube { get; set; }

    [Field(
       Title = "Link instagram",
       Required = false,
       Control = InputControlType.TextBox)]
    public string LinkInstagram { get; set; }

    [Field(Title = "Danh sách menu", ChildTitle = "Menu chính")]
    public MenuItem[] MainMenus { get; set; }

    [Field(Title = "Thông tin công ty")]
    public CompanyInfo Company { get; set; }    
}

public class MenuItem
{
    [Field(
       Title = "en:Title|vi:Tiêu đề",
       Required = false,
       Control = InputControlType.TextBox)]
    public string Title { get; set; }

    [Field(
       Title = "en:Link|vi:Link",
       Required = false,
       Control = InputControlType.Link)]
    public string Href { get; set; }

    [Field(Title = "Các menu con", ChildTitle = "Menu con")]
    public MenuItem[] SubMenus { get; set; }
}

public class CompanyInfo
{
    [Field(
        Title = "en:Slogan|vi:Slogan",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Slogan { get; set; }

    [Field(
        Title = "en:Company name|vi:Tên công ty",
        Required = false,
        Control = InputControlType.TextBox)]
    public string CompanyName { get; set; }
    [Field(
       Title = "Link cấu hình đăng ký",
       Required = false,
       Control = InputControlType.TextBox)] 
    public string RegsinLink { get; set; }


    [Field(
        Title = "en:Bản đồ|vi:Bản đồ",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Maps { get; set; }

    [Field(
        Title = "Hotline",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Hotline { get; set; }
    [Field(
       Title = "Zalo",
       Required = false,
       Control = InputControlType.TextBox)]
    public string Zalo { get; set; }
    [Field(
   Title = "Whatsapp",
   Required = false,
   Control = InputControlType.TextBox)]
    public string Whatsapp { get; set; }
    [Field(
        Title = "Số điện thoại",
        Required = false,
        Control = InputControlType.TextBox)]
    public string ContactPhone { get; set; }

    [Field(
        Title = "Số liên lạc",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Phone { get; set; }

    [Field(
      Title = "Website",
      Required = false,
      Control = InputControlType.TextBox)]
    public string Website { get; set; }

    [Field(
      Title = "Email",
      Required = false,
      Control = InputControlType.TextBox)]
    public string Email { get; set; }

    [Field(
        Title = "Copyright Text",
        Required = false,
        Control = InputControlType.TextBox)]
    public string CopyrightText { get; set; }


    [Field(
        Title = "en:Địa chỉ|vi:Địa chỉ",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Address { get; set; }

    [Field(
        Title = "en:Đường dẫn đăng nhập|vi:Đường dẫn đăng nhập",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Login { get; set; }   

}
