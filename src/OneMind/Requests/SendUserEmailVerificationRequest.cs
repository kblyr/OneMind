namespace OneMind.Requests;

public record SendUserEmailVerificationRequest : OriginTokenHolderBase, IRequest<Guid>
{
    public int Id { get; init; }
    public string EmailAddress { get; init; } = "";
    public bool WithPassword { get; init; }
    public string? Password { get; init; }
}