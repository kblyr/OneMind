namespace OneMind.Data.Entities;

record Organization
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string? Description { get; init; }
    public int LeaderId { get; init; }

    public User Leader { get; init; } = default!;
}