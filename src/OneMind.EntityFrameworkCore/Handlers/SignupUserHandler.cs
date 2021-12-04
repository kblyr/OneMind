namespace OneMind.Handlers;

sealed class SignupUserHandler : IRequestHandler<SignupUserRequest, int>
{
    readonly IDbContextFactory<OneMindDbContext> _contextFactory;
    readonly InsertUser _insertUser;
    readonly IUserPasswordCryptoService _passwordCryptoService;

    public SignupUserHandler(IDbContextFactory<OneMindDbContext> contextFactory, InsertUser insertUser, IUserPasswordCryptoService passwordCryptoService)
    {
        _contextFactory = contextFactory;
        _insertUser = insertUser;
        _passwordCryptoService = passwordCryptoService;
    }

    public async Task<int> Handle(SignupUserRequest request, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

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
        );

        if (id == 0)
            throw new UserCreationFailedException("Failed to sign-up user");

        await transaction.CommitAsync(cancellationToken);
        return id;
    }
}
