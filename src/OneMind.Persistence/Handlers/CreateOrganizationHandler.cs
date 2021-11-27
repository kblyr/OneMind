namespace OneMind.Handlers;

sealed class CreateOrganizationHandler : IRequestHandler<CreateOrganizationRequest, int>
{
    readonly IDbContextFactory<OneMindDbContext> _contextFactory;
    readonly InsertOrganization _insertOrganization;

    public CreateOrganizationHandler(IDbContextFactory<OneMindDbContext> contextFactory, InsertOrganization insertOrganization)
    {
        _contextFactory = contextFactory;
        _insertOrganization = insertOrganization;
    }

    public async Task<int> Handle(CreateOrganizationRequest request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var leader =  await context.Users.GetRequiredAsync(request.LeaderId, cancellationToken);

        var id = await _insertOrganization.ExecuteAsync(
            context.WithHotSave(),
            new()
            {
                Name = request.Name,
                Description = request.Description,
                Leader = leader,
                LeaderId = leader.Id
            },
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        return id;
    }
}
