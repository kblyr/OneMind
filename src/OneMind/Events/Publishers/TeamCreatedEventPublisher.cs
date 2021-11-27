namespace OneMind.Events.Publishers;

sealed class TeamCreatedEventPublisher : IPipelineBehavior<CreateTeamRequest, int>
{
    readonly IMediator _mediator;

    public TeamCreatedEventPublisher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<int> Handle(CreateTeamRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<int> next)
    {
        var id = await next();
        await PublishAsync(id, cancellationToken);
        return id;
    }

    async Task PublishAsync(int id, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new TeamCreated { Id = id }, cancellationToken);
    }
}
