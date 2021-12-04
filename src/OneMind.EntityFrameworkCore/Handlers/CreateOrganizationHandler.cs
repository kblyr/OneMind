namespace OneMind.Handlers;

sealed class CreateOrganizationHandler : IRequestHandler<CreateOrganizationRequest, int>
{
    readonly IDbContextFactory<OneMindDbContext> _contextFactory;
    readonly InsertOrganization _insertOrganization;
    readonly InsertOrganizationInvitation _insertInvitation;
    readonly IMediator _mediator;

    public CreateOrganizationHandler(IDbContextFactory<OneMindDbContext> contextFactory, InsertOrganization insertOrganization, InsertOrganizationInvitation insertInvitation, IMediator mediator)
    {
        _contextFactory = contextFactory;
        _insertOrganization = insertOrganization;
        _insertInvitation = insertInvitation;
        _mediator = mediator;
    }

    public async Task<int> Handle(CreateOrganizationRequest request, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var leader = await GetLeaderAsync(context, request, cancellationToken);
        var creator = await GetCreatorAsync(context, request, cancellationToken);
        var id = await InsertOrganizationAsync(context, request, leader, creator, cancellationToken);

        var organization = await GetOrganizationAsync(context, id, cancellationToken);
        await InsertInvitationsAsync(context, organization, leader, request.InvitedMemberIds, cancellationToken);

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

    async Task InsertInvitationsAsync(OneMindDbContext context, Organization organization, User sender, IEnumerable<int> recipientIds, CancellationToken cancellationToken)
    {
        foreach (var recipientId in recipientIds)
        {
            var recipient = await GetInvitationRecipientAsync(context, recipientId, cancellationToken);
            await _insertInvitation.ExecuteAsync(
                context.WithoutHotSave(),
                new()
                {
                    OrganizationId = organization.Id,
                    InvitedOn = DateTimeOffset.Now,
                    SenderId = sender.Id,
                    RecipientId = recipient.Id,
                },
                cancellationToken
            );
        }
    }

    static async Task<Organization> GetOrganizationAsync(OneMindDbContext context, int id, CancellationToken cancellationToken) => await context.Organizations
        .AsNoTracking()
        .Where(organization => organization.Id == id)
        .Select(organization => new Organization
        {
            Id = organization.Id,
            Name = organization.Name,
        })
        .SingleOrDefaultAsync(cancellationToken)
        ?? throw new OrganizationNotFoundException { Id = id };

    static async Task<User> GetLeaderAsync(OneMindDbContext context, CreateOrganizationRequest request, CancellationToken cancellationToken) => await context.Users
        .AsNoTracking()
        .Where(user => user.Id == request.LeaderId)
        .Select(user => new User
        {
            Id = user.Id,
            Username = user.Username,
        })
        .SingleOrDefaultAsync(cancellationToken)
        ?? throw new OrganizationLeaderRequiredException
        {
            Organization = new()
            {
                Name = request.Name
            }
        };

    static async Task<User> GetCreatorAsync(OneMindDbContext context, CreateOrganizationRequest request, CancellationToken cancellationToken) => await context.Users
        .AsNoTracking()
        .Where(user => user.Id == request.CreatedById)
        .Select(user => new User
        {
            Id = user.Id,
            Username = user.Username,
        })
        .SingleOrDefaultAsync(cancellationToken)
        ?? throw new OrganizationCreatorRequiredException
        {
            Organization = new()
            {
                Name = request.Name
            }
        };

    static async Task<User> GetInvitationRecipientAsync(OneMindDbContext context, int recipientId, CancellationToken cancellationToken) => await context.Users
        .AsNoTracking()
        .Where(user => user.Id == recipientId)
        .Select(user => new User
        {
            Id = user.Id,
            Username = user.Username,
        })
        .SingleOrDefaultAsync(cancellationToken)
        ?? throw new UserNotFoundException { Id = recipientId };
}
