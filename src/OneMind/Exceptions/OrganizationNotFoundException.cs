namespace OneMind.Exceptions;

public class OrganizationNotFoundException : CodeCompanionException
{
    public int Id { get; init; }

    protected override void SetClientMessage(StringBuilder builder) => builder.Append("Organization does not exists");

    protected override void SetErrorData(IDictionary<string, object?> errorData) => errorData.FluentAdd(nameof(Id), Id);
}
