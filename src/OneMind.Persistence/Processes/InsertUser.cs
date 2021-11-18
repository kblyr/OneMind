using CodeCompanion.Processes;
using OneMind.Data;
using OneMind.Data.Entities;
using OneMind.Exceptions;

namespace OneMind.Processes
{
    sealed class InsertUser : IAsyncProcess<User, int>
    {
        public async Task<int> ExecuteAsync(IProcessContext processContext, User user, CancellationToken cancellationToken = default)
        {
            if (processContext is OneMindDbContext context)
            {
                if (await context.Users.UsernameExistsAsync(user.Username, cancellationToken))
                    throw new UsernameAlreadyExistsException { Username = user.Username };

                context.Users.Add(user);
                await context.SaveChangesAsync(cancellationToken);
                return user.Id;
            }

            throw InvalidProcessContextException.Expects<OneMindDbContext>();
        }
    }
}