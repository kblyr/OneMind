namespace OneMind.Entities;

record OrganizationInvitation
{
    public long Id { get; init; }
    public int OrganizationId { get; init; }
    public DateTimeOffset InvitedOn { get; init; }
    public int SenderId { get; init; }
    public int RecipientId { get; init; }
    public string? Message { get; init; }
    public InvitationStatus Status { get; init; }
    public DateTimeOffset? AcceptedOn { get; init; }
    public DateTimeOffset? RejectedOn { get; init; }

    public Organization Organization { get; init; } = default!;
    public User Sender { get; init; } = default!;
    public User Recipient { get; init; } = default!;
}
