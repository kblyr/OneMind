namespace OneMind.Entities;

record TeamMember
{
    public long Id { get; init; }
    public int TeamId { get; init; }
    public int MemberId { get; init; }
    public DateTime JoinedOn { get; init; }

    public Team Team { get; init; } = default!;
    public User Member { get; init; } = default!;
}