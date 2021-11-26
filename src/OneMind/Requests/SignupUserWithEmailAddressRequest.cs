namespace OneMind.Requests;

public record SignupUserWithEmailAddressRequest : IRequest<int>
{
    public string EmailAddress { get; set; } = "";
}
