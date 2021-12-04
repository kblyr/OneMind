namespace OneMind.Events;

public record UserCreateFailed : INotification
{
    public string ErrorMessage { get; init; } = "";
}