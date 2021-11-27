namespace OneMind.Events;

public record MailSentEvent : INotification
{
    public Guid Id { get; init; }
}
