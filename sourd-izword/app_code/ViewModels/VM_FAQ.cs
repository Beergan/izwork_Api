using SLK.Common;

public class VM_FAQ
{
    [Field(
        Title = "en:Question|vi:Câu hỏi",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Question { get; set; }

    [Field(
        Title = "en:Answer|vi:Câu trả lời",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Answer { get; set; }

}