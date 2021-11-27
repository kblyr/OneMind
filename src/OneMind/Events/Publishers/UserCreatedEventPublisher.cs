namespace OneMind.Events.Publishers;

sealed class UserCreatedEventPublisher :
    IPipelineBehavior<SignupUserRequest, int>,
    IPipelineBehavior<SignupUserWithEmailAddressRequest, int>
{
    readonly IMediator _mediator;

    public UserCreatedEventPublisher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<int> Handle(SignupUserRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<int> next)
    {
        var id = await next();
        await PublishAsync(id, cancellationToken);
        return id;
    }

    public async Task<int> Handle(SignupUserWithEmailAddressRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<int> next)
    {
        var id = await next();
        await PublishAsync(id, cancellationToken);
        return id;
    }

    async Task PublishAsync(int id, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new UserCreated { Id = id }, cancellationToken);
    }
}
