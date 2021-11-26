namespace OneMind.Handlers;

sealed class SignupUserWithEmailAddressHandler : IRequestHandler<SignupUserWithEmailAddressRequest, int>
{
    readonly IDbContextFactory<OneMindDbContext> _contextFactory;
    readonly InsertUser _insertUser;
    readonly IUserPasswordCryptoService _passwordCryptoService;
    readonly IMediator _mediator;
    readonly IUsernameFromEmailAddressExtractor _usernameFromEmailAddressExtractor;
    readonly IUserPasswordGenerator _passwordGenerator;

    public SignupUserWithEmailAddressHandler(IDbContextFactory<OneMindDbContext> contextFactory, InsertUser insertUser, IUserPasswordCryptoService passwordCryptoService, IMediator mediator, IUsernameFromEmailAddressExtractor usernameFromEmailAddressExtractor, IUserPasswordGenerator passwordGenerator)
    {
        _contextFactory = contextFactory;
        _insertUser = insertUser;
        _passwordCryptoService = passwordCryptoService;
        _mediator = mediator;
        _usernameFromEmailAddressExtractor = usernameFromEmailAddressExtractor;
        _passwordGenerator = passwordGenerator;
    }

    public async Task<int> Handle(SignupUserWithEmailAddressRequest request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        var username = _usernameFromEmailAddressExtractor.Extract(request.EmailAddress);
        var password = _passwordGenerator.Generate();

        var id = await _insertUser.ExecuteAsync(
            context.WithHotSave(),
            new()
            {
                Username = username,
                EmailAddress = request.EmailAddress,
                HashedPassword = _passwordCryptoService.ComputeHash(password),
                IsEmailVerified = false,
                IsPasswordChangeRequired = false,
            }
        ).ConfigureAwait(false);

        if (id != 0)
        {
            var sendUserEmailVerificationRequest = new SendUserEmailVerificationRequest
            {
                Id = id,
                EmailAddress = request.EmailAddress,
                WithPassword = true,
                Password = password
            };

            await _mediator.Send(sendUserEmailVerificationRequest, cancellationToken).ConfigureAwait(false);
        }
        
        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        return id;
    }
}
