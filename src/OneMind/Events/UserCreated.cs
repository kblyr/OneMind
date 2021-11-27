namespace OneMind.Events;

public record UserCreated : INotification
{
    public int Id { get; init; }
}
