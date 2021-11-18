using CodeCompanion.EntityFrameworkCore;
using CodeCompanion.Processes;
using Microsoft.EntityFrameworkCore;
using OneMind.Data.Entities;

namespace OneMind.Data
{
    public sealed class OneMindDbContext : CodeCompanionDbContext, IProcessContext
    {
        public OneMindDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
    }
}