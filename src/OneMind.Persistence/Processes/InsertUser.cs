namespace OneMind.Processes;

sealed class InsertUser : IAsyncProcess<User, int>
{
    public async Task<int> ExecuteAsync(IProcessContext processContext, User user, CancellationToken cancellationToken = default)
    {
        if (processContext is not OneMindDbContext context)
            throw InvalidProcessContextException.Expects<OneMindDbContext>();

        if (await context.Users.UsernameExistsAsync(user.Username, cancellationToken).ConfigureAwait(false))
            throw new UsernameAlreadyExistsException { Username = user.Username };

        if (await context.Users.EmailAddressExistsAsync(user.EmailAddress, cancellationToken).ConfigureAwait(false))
            throw new UserEmailAddressAlreadyExistsException { EmailAddress = user.EmailAddress };

        context.Users.Add(user, context.CurrentFootprint);
        await context.TrySaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return user.Id;
    }
}