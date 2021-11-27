namespace OneMind.Events;

public record TeamCreatedEvent : INotification
{
    public int Id { get; init; }
}