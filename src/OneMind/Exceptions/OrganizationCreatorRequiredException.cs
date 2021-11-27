namespace OneMind.Exceptions;

public class OrganizationCreatorRequiredException : CodeCompanionException
{
    public OrganizationObj Organization { get; init; } = default!;

    protected override void SetClientMessage(StringBuilder builder) => builder.Append($"Creator of organization '{Organization.Name}' is required");

    protected override void SetErrorData(IDictionary<string, object?> errorData) => errorData.FluentAdd(nameof(Organization), Organization);

    public record OrganizationObj
    {
        public int? Id { get; init; }
        public string Name { get; init; } = "";
    }
}
