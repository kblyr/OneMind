namespace OneMind.Handlers;

sealed class CreateTeamHandler : IRequestHandler<CreateTeamRequest, int>
{
    readonly IDbContextFactory<OneMindDbContext> _contextFactory;
    readonly InsertTeam _insertTeam;

    public CreateTeamHandler(IDbContextFactory<OneMindDbContext> contextFactory, InsertTeam insertTeam)
    {
        _contextFactory = contextFactory;
        _insertTeam = insertTeam;
    }

    public async Task<int> Handle(CreateTeamRequest request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var leader = await GetLeaderAsync(context, request, cancellationToken);
        var organization = await context.Organizations.GetAsync(request.OrganizationId ?? 0, cancellationToken);
        var id = await InsertTeamAsync(context, request, leader, organization, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return id;
    }

    async Task<int> InsertTeamAsync(OneMindDbContext context, CreateTeamRequest request, User leader, Organization? organization, CancellationToken cancellationToken) => await _insertTeam.ExecuteAsync(
        context.WithHotSave(),
        new()
        {
            Name = request.Name,
            Description = request.Description,
            Leader = leader,
            LeaderId = leader.Id,
            Visibility = (TeamVisibility)request.Visibility,
            Organization = organization,
            OrganizationId = organization?.Id
        },
        cancellationToken
    );

    static async Task<User> GetLeaderAsync(OneMindDbContext context, CreateTeamRequest request, CancellationToken cancellationToken) => await context.Users.GetAsync(request.LeaderId, cancellationToken)
        ?? throw new TeamLeaderRequiredException
        {
            Team = new()
            {
                Name = request.Name
            }
        };
}
