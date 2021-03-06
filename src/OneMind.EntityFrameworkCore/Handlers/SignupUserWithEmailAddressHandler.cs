namespace OneMind.Handlers;

sealed class SignupUserWithEmailAddressHandler : IRequestHandler<SignupUserWithEmailAddressRequest, int>
{
    readonly IDbContextFactory<OneMindDbContext> _contextFactory;
    readonly InsertUser _insertUser;
    readonly IUserPasswordCryptoService _passwordCryptoService;
    readonly IUsernameFromEmailAddressExtractor _usernameFromEmailAddressExtractor;
    readonly IUserPasswordGenerator _passwordGenerator;

    public SignupUserWithEmailAddressHandler(IDbContextFactory<OneMindDbContext> contextFactory, InsertUser insertUser, IUserPasswordCryptoService passwordCryptoService, IUsernameFromEmailAddressExtractor usernameFromEmailAddressExtractor, IUserPasswordGenerator passwordGenerator)
    {
        _contextFactory = contextFactory;
        _insertUser = insertUser;
        _passwordCryptoService = passwordCryptoService;
        _usernameFromEmailAddressExtractor = usernameFromEmailAddressExtractor;
        _passwordGenerator = passwordGenerator;
    }

    public async Task<int> Handle(SignupUserWithEmailAddressRequest request, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

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
            },
            cancellationToken
        );

        if (id == 0)
            throw new UserCreationFailedException("Failed to sign-up user");

        await transaction.CommitAsync(cancellationToken);
        return id;
    }
}
