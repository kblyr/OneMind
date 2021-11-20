namespace OneMind.Requests;

public record SignupUserWithEmailAddressRequest : OriginTokenHolderBase, IRequest<int>
{
    public string EmailAddress { get; set; } = "";
}
