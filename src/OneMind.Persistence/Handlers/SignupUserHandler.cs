using MediatR;
using Microsoft.EntityFrameworkCore;
using OneMind.Data;
using OneMind.Processes;
using OneMind.Requests;

namespace OneMind.Handlers
{
    sealed class SignupUserHandler : IRequestHandler<SignupUserRequest, int>
    {
        readonly IDbContextFactory<OneMindDbContext> _contextFactory;
        readonly InsertUser _insertUser;

        public SignupUserHandler(IDbContextFactory<OneMindDbContext> contextFactory, InsertUser insertUser)
        {
            _contextFactory = contextFactory;
            _insertUser = insertUser;
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
                }
            );

            await transaction.CommitAsync(cancellationToken);
            return 0;
        }
    }
}