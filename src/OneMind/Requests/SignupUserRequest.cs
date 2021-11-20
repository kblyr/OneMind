namespace OneMind.Requests;

public record SignupUserRequest : IRequest<int>
{
    public string Username { get; init; } = "";
    public string EmailAddress { get; init; } = "";
    public string Password { get; init; } = "";
}