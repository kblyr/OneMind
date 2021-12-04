namespace OneMind.Processes;

sealed class InsertOrganizationInvitation : IAsyncProcess<OrganizationInvitation, long>
{
    public async Task<long> ExecuteAsync(IProcessContext processContext, OrganizationInvitation organizationInvitation, CancellationToken cancellationToken = default)
    {
        if (processContext is not OneMindDbContext context)
            throw InvalidProcessContextException.Expects<OneMindDbContext>();

        context.OrganizationInvitations.Add(organizationInvitation, context.CurrentFootprint);
        await context.TrySaveChangesAsync(cancellationToken);
        return organizationInvitation.Id;
    }
}
