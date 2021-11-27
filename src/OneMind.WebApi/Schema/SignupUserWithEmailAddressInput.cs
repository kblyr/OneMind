namespace OneMind.Schema;

[SchemaId("User.SignupWithEmailAddress(IN)")]
public sealed class SignupUserWithEmailAddressInput
{
    public string EmailAddress { get; init; } = "";

    public SignupUserWithEmailAddressRequest ToRequest() => new()
    {
        EmailAddress = EmailAddress,
    };
}