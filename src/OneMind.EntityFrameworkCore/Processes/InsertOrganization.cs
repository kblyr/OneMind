namespace OneMind.Processes;

sealed class InsertOrganization : IAsyncProcess<Organization, int>
{
    public async Task<int> ExecuteAsync(IProcessContext processContext, Organization organization, CancellationToken cancellationToken = default)
    {
        if (processContext is not OneMindDbContext context)
            throw InvalidProcessContextException.Expects<OneMindDbContext>();

        context.Organizations.Add(organization, context.CurrentFootprint);
        await context.TrySaveChangesAsync(cancellationToken);
        return organization.Id;
    }
}
