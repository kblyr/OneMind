namespace OneMind.Requests;

public record CreateOrganizationRequest : IRequest<int>
{
    public string Name { get; init; } = "";
    public string? Description { get; init; }
    public int LeaderId { get; init; }
}