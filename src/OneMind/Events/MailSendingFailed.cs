namespace OneMind.Events;

public record MailSendingFailed : INotification
{
    public Guid Id { get; init; }
    public string? ErrorMessage { get; init; }
}