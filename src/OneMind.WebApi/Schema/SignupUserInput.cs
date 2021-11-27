namespace OneMind.Schema;

[SchemaId("User.Signup(IN)")]
public sealed class SignupUserInput
{
    public string Username { get; init; } = "";
    public string EmailAddress { get; init; } = "";
    public string Password { get; init; } = "";
    public bool IsEmailVerified { get; init; }
    public bool IsPasswordChangeRequired { get; init; }

    public SignupUserRequest ToRequest() => new()
    {
        Username = Username,
        EmailAddress = EmailAddress,
        Password = Password,
        IsEmailVerified = IsEmailVerified,
        IsPasswordChangeRequired = IsPasswordChangeRequired,
    };
}
