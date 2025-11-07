using SLK.Common;

public class VM_Blog : VM_Base
{
    [Field(
       Title = "en:Title widget adds|vi:Tiêu đề tiện ích bổ sung",
       Required = false,
       Control = InputControlType.TextBox)]
    public string TitleWidget { get; set; }

    [Field(
      Title = "en:Link widget adds|vi:Đường dẫn tiện ích",
      Required = false,
      Control = InputControlType.Link)]
    public string HrefWidget { get; set; }

    [Field(
        Title = "en:Link button share|vi:Đường dẫn nút chia sẻ",
        Required = false,
        Control = InputControlType.Link)]
    public string HrefShare { get; set; }

    [Field(
        Title = "en:Author|vi:Tác giả",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Author { get; set; }

    [Field(
        Title = "en:Content top|vi:Nội dung trên",
        Required = false,
        Control = InputControlType.RichTextBox)]
    public string ContentTop { get; set; }

    [Field(
        Title = "en:Video Background|vi:Ảnh nền video",
        Required = false,
        Control = InputControlType.Image)]
    public string BgVideo { get; set; }

    [Field(
        Title = "en:Link Video|vi:Đường dẫn video",
        Required = false,
        Control = InputControlType.TextBox)]
    public string HrefVideo { get; set; }

    [Field(
        Title = "en:Video Description|vi:Mô tả video",
        Required = false,
        Control = InputControlType.TextBox)]
    public string DescVideo { get; set; }

    [Field(
        Title = "en:Content bottom|vi:Nội dung dưới",
        Required = false,
        Control = InputControlType.RichTextBox)]
    public string ContentBottom { get; set; }

}