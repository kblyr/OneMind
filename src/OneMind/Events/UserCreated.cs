namespace OneMind.Events;

public record UserCreated : INotification
{
    public int Id { get; init; }
    public string Username { get; init; } = "";
    public string EmailAddress { get; init; } = "";
}
