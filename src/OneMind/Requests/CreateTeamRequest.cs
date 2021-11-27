namespace OneMind.Requests;

public record CreateTeamRequest : IRequest<int>
{
    public string Name { get; init; } = "";
    public string? Description { get; init; }
    public int LeaderId { get; init; }
    public short Visibility { get; init; }
    public int? OrganizationId { get; init; }
}