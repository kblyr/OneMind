namespace OneMind.Mailing;

public record MailSettings
{
    public string Server { get; init; } = "";
    public int Port { get; init; }
    public bool IsSslEnabled { get; init; }
    public bool UseDefaultCredentials { get; init; }
}