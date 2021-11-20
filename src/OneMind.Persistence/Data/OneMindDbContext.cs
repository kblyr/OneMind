namespace OneMind.Data;

sealed class OneMindDbContext : CodeCompanionDbContext, IProcessContext
{
    public OneMindDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
}