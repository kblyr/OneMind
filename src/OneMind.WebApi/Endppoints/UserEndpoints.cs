namespace OneMind.Endpoints;

static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/users/signup", Signup)
            .WithTags("User")
            .Accepts<SignupUserRequest>(MediaTypeNames.Application.Json)
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        builder.MapPost("/users/signup/email", SignupWithEmailAddress)
            .WithTags("User")
            .Accepts<SignupUserWithEmailAddressRequest>(MediaTypeNames.Application.Json)
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        return builder;
    }

    static async Task<IResult> Signup(IMediator mediator, [FromBody] SignupUserRequest request, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(request, cancellationToken);
        return Results.Created($"/users/{id}", id);
    }

    static async Task<IResult> SignupWithEmailAddress(IMediator mediator, [FromBody] SignupUserWithEmailAddressRequest request, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(request, cancellationToken);
        return Results.Created($"/users/{id}", id);
    }
}