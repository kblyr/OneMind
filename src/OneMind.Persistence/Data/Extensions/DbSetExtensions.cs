using Microsoft.EntityFrameworkCore;
using OneMind.Data.Entities;

namespace OneMind.Data;

static class DbSetExtensions
{
    public static async Task<bool> UsernameExistsAsync(this DbSet<User> users, string username, CancellationToken cancellationToken = default) => await users
        .AsNoTracking()
        .Where(user => user.Username == username)
        .AnyAsync(cancellationToken);

    public static async Task<bool> EmailAddressExistsAsync(this DbSet<User> users, string emailAddress, CancellationToken cancellationToken = default) => await users
        .AsNoTracking()
        .Where(user => user.EmailAddress == emailAddress)
        .AnyAsync(cancellationToken);
}