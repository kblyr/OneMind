namespace OneMind.Mailing;

public record Mail
{
    public string Subject { get; init; } = "";
    public MailSettings Settings { get; init; } = default!;
    public MailAccount Sender { get; init; } = default!;
    public IEnumerable<MailAccount> Recipients { get; init; } = Enumerable.Empty<MailAccount>();
    public MailBody Body { get; init; } = default!;
}
