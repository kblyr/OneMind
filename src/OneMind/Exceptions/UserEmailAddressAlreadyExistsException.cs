namespace OneMind.Exceptions;

public class UserEmailAddressAlreadyExistsException : CodeCompanionException
{
    public string EmailAddress { get; init; } = "";

    protected override void SetClientMessage(StringBuilder builder) => builder.Append($"User with email address '{EmailAddress}' already exists");

    protected override void SetErrorData(IDictionary<string, object?> errorData) => errorData.FluentAdd(nameof(EmailAddress), EmailAddress);
}
