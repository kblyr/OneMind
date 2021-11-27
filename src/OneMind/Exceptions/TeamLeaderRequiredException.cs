namespace OneMind.Exceptions;

public class TeamLeaderRequiredException : CodeCompanionException
{
    public TeamObj Team { get; init; } = default!;

    protected override void SetClientMessage(StringBuilder builder) => builder.Append($"Leader for team '{Team.Name}' is required");

    protected override void SetErrorData(IDictionary<string, object?> errorData) => errorData.FluentAdd(nameof(Team), Team);

    public record TeamObj
    {
        public int? Id { get; init; }
        public string Name { get; init; } = "";
    }
}