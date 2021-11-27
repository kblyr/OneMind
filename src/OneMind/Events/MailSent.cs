namespace OneMind.Events;

public record MailSent : INotification
{
    public Guid Id { get; init; }
}
