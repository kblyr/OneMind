namespace OneMind.Requests;

public record CreateOrganizationRequest : IRequest<int>
{
    public string Name { get; init; } = "";
    public string? Description { get; init; }
    public int LeaderId { get; init; }
    public short Visibility { get; init; }
    public int CreatedById { get; init; }
    public DateTime CreatedOn { get; init; }
}
