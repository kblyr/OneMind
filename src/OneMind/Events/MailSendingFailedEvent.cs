namespace OneMind.Events;

public record MailSendingFailedEvent : INotification
{
    public Guid Id { get; init; }
    public string? ErrorMessage { get; init; }
}