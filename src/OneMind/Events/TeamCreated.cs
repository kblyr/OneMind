namespace OneMind.Events;

public record TeamCreated : INotification
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
}