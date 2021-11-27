namespace OneMind.Data.Entities;

record OrganizationMember
{
    public long Id { get; init; }
    public int OrganizationId { get; init; }
    public int MemberId { get; init; }
    public DateTime JoinedOn { get; init; }

    public Organization Organization { get; init; } = default!;
    public User Member { get; init; } = default!;
}
