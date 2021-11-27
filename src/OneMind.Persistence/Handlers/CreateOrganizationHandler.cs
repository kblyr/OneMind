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

        var leader = await GetLeaderAsync(context, request, cancellationToken);
        var creator = await GetCreatorAsync(context, request, cancellationToken);
        var id = await InsertOrganizationAsync(context, request, leader, creator, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return id;
    }

    async Task<int> InsertOrganizationAsync(OneMindDbContext context, CreateOrganizationRequest request, User leader, User creator, CancellationToken cancellationToken) => await _insertOrganization.ExecuteAsync(
        context.WithHotSave(),
        new()
        {
            Name = request.Name,
            Description = request.Description,
            Leader = leader,
            LeaderId = leader.Id,
            Visibility = (OrganizationVisibility)request.Visibility,
            CreaatedBy = creator,
            CreatedById = creator.Id,
            CreatedOn = request.CreatedOn
        },
        cancellationToken
    );

    static async Task<User> GetLeaderAsync(OneMindDbContext context, CreateOrganizationRequest request, CancellationToken cancellationToken) => await context.Users.GetAsync(request.LeaderId, cancellationToken)
        ?? throw new OrganizationLeaderRequiredException
        {
            Organization = new()
            {
                Name = request.Name
            }
        };

    static async Task<User> GetCreatorAsync(OneMindDbContext context, CreateOrganizationRequest request, CancellationToken cancellationToken) => await context.Users.GetAsync(request.CreatedById, cancellationToken)
        ?? throw new OrganizationCreatorRequiredException
        {
            Organization = new()
            {
                Name = request.Name
            }
        };
}
