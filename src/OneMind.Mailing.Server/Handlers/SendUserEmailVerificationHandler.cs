using Microsoft.Extensions.Options;

namespace OneMind.Handlers;

sealed class SendUserEmailVerificationHandler : IRequestHandler<SendUserEmailVerificationRequest, Guid>
{
    readonly IOptions<MailingOptions> _mailingOptions;

    public SendUserEmailVerificationHandler(IOptions<MailingOptions> mailingOptions)
    {
        _mailingOptions = mailingOptions;
    }

    public async Task<Guid> Handle(SendUserEmailVerificationRequest request, CancellationToken cancellationToken)
    {
        var mailingOptions = _mailingOptions.Value;
        throw new NotImplementedException();
    }
}