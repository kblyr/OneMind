namespace OneMind.Mailing;

public record MailBody
{
    public string Content { get; init; } = "";
    public bool IsHtml { get; init; }
}