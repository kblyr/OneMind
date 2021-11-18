using Microsoft.EntityFrameworkCore;
using OneMind.Data.Entities;

namespace OneMind.Data
{
    static class DbSetExtensions
    {
        public static async Task<bool> UsernameExistsAsync(this DbSet<User> users, string username, CancellationToken cancellationToken = default) => await users
            .AsNoTracking()
            .Where(user => user.Username == username)
            .AnyAsync(cancellationToken);
    }
}