namespace OneMind.Exceptions;

public class UserNotFoundException : CodeCompanionException
{
    public int Id { get; init; }

    protected override void SetClientMessage(StringBuilder builder) => builder.Append("User does not exists");

    protected override void SetErrorData(IDictionary<string, object?> errorData) => errorData.FluentAdd(nameof(Id), Id);
}
