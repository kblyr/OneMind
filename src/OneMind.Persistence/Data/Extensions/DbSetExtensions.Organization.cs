namespace OneMind.Data;

partial class DbSetExtensions
{
    public static async Task<Organization?> GetAsync(this DbSet<Organization> organizations, int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return null;

        return await organizations.FindByIdAsync(id, cancellationToken) ?? throw new OrganizationNotFoundException { Id = id };
    }
}