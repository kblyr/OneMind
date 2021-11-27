namespace OneMind.Events;

public record UserCreatedEvent : INotification
{
    public int Id { get; init; }
}
