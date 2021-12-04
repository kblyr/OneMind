﻿using System.Net.Mime;

namespace OneMind.Endpoints;

static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/users/signup", Signup)
            .WithTags("User")
            .Accepts<SignupUserInput>(MediaTypeNames.Application.Json)
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        builder.MapPost("/users/signup/email", SignupWithEmailAddress)
            .WithTags("User")
            .Accepts<SignupUserWithEmailAddressInput>(MediaTypeNames.Application.Json)
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        return builder;
    }

    static async Task<IResult> Signup(IMediator mediator, [FromBody]SignupUserInput input, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(input.ToRequest(), cancellationToken);
        return Results.Created($"/users/{id}", id);
    }

    static async Task<IResult> SignupWithEmailAddress(IMediator mediator, [FromBody]SignupUserWithEmailAddressInput input, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(input.ToRequest(), cancellationToken);
        return Results.Created($"/users/{id}", id);
    }
}