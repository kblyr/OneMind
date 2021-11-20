namespace OneMind.Handlers;

sealed class SignupUserHandler : IRequestHandler<SignupUserRequest, int>
{
    readonly IDbContextFactory<OneMindDbContext> _contextFactory;
    readonly InsertUser _insertUser;
    readonly IUserPasswordCryptoService _passwordCryptoService;
    readonly IMediator _mediator;

    public SignupUserHandler(IDbContextFactory<OneMindDbContext> contextFactory, InsertUser insertUser, IUserPasswordCryptoService passwordCryptoService, IMediator mediator)
    {
        _contextFactory = contextFactory;
        _insertUser = insertUser;
        _passwordCryptoService = passwordCryptoService;
        _mediator = mediator;
    }

    public async Task<int> Handle(SignupUserRequest request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        var id = await _insertUser.ExecuteAsync(
            context.WithHotSave(),
            new()
            {
                Username = request.Username,
                EmailAddress = request.EmailAddress,
                HashedPassword = _passwordCryptoService.ComputeHash(request.Password),
                IsEmailVerified = request.IsEmailVerified,
                IsPasswordChangeRequired = request.IsPasswordChangeRequired,
            },
            cancellationToken
        ).ConfigureAwait(false);

        if (id != 0)
        {
            var sendUserEmailVerificationRequest = new SendUserEmailVerificationRequest
            {
                OriginToken = request.OriginToken,
                Id = id,
                EmailAddress = request.EmailAddress,
                WithPassword = false,
                Password = null
            };

            await _mediator.Send(sendUserEmailVerificationRequest, cancellationToken).ConfigureAwait(false);
        }

        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        return id;
    }
}