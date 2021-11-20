using OneMind.Security;

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
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var id = await _insertUser.ExecuteAsync(
            context,
            new()
            {
                Username = request.Username,
                EmailAddress = request.EmailAddress,
                HashedPassword = _passwordCryptoService.ComputeHash(request.Password)
            },
            cancellationToken
        );

        await transaction.CommitAsync(cancellationToken);
        return id;
    }
}