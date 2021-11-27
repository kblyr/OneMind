namespace OneMind.Data;

static class DbSetExtensions
{
    public static async Task<User?> GetAsync(this DbSet<User> users, int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return null;

        return await users.FindByIdAsync(id, cancellationToken);
    }

    public static async Task<User> GetRequiredAsync(this DbSet<User> users, int id, CancellationToken cancellationToken = default) => await users.GetAsync(id, cancellationToken)
        ?? throw new UserNotFoundException
        {
            Id = id
        };

    public static async Task<bool> UsernameExistsAsync(this DbSet<User> users, string username, CancellationToken cancellationToken = default) => await users
        .AsNoTracking()
        .Where(user => user.Username == username)
        .AnyAsync(cancellationToken);

    public static async Task<bool> EmailAddressExistsAsync(this DbSet<User> users, string emailAddress, CancellationToken cancellationToken = default) => await users
        .AsNoTracking()
        .Where(user => user.EmailAddress == emailAddress)
        .AnyAsync(cancellationToken);
}