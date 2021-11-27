namespace OneMind.Data.Entities;

record Team
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string? Description { get; init; }
    public int LeaderId { get; init; }
    public TeamVisibility Visibility { get; init; }
    public int? OrganizationId { get; init; }

    public User Leader { get; init; } = default!;
    public Organization? Organization { get; init; }
}
