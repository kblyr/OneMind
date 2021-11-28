namespace OneMind.Requests;

public record SendMailOrganizationInvitationRequest : IRequest<Guid>
{
    public int OrganizationId { get; init; }
    public int RecipientId { get; init; }
    public string? Message { get; init; }
}
