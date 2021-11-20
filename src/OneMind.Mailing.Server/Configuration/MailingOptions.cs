namespace OneMind.Configuration;

public sealed class MailingOptions
{
    public _SmtpOptions Smtp { get; init; } = default!;
    public string EmailAddress { get; init; } = "";
    public string Password { get; init; } = "";
    public string Name { get; init; } = "";

    public sealed class _SmtpOptions
    {
        public string Host { get; init; } = "";
        public int Port { get; init; }
    }

}
