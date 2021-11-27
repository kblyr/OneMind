namespace OneMind.Events.Publishers;

sealed class OrganizationCreatedEventPublisher : IPipelineBehavior<CreateOrganizationRequest, int>
{
    readonly IMediator _mediator;

    public OrganizationCreatedEventPublisher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<int> Handle(CreateOrganizationRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<int> next)
    {
        var id = await next();
        await PublishAsync(id, cancellationToken);
        return id;
    }

    private async Task PublishAsync(int id, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new OrganizationCreatedEvent { Id = id }, cancellationToken);
    }
}