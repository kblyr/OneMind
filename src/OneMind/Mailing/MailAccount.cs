namespace OneMind.Mailing;

public record MailAccount
{
    public string EmailAddress { get; init; } = "";
    public string Name { get; init; } = "";
    public string? Password { get; init; }
}
