using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Api;

public static class UserApi
{
    public static WebApplication UseUsersApi(this WebApplication app)
    {
        app.MapGet("api/users/me", async (HttpContext context, IQueryHandler<GetUser, UserDTO> handler) =>
        {
            var userDto = await handler.HandleAsync(new GetUser { UserId = Guid.Parse(context.User.Identity?.Name) });
            return Results.Ok(userDto);
        })
        .RequireAuthorization().WithName("me");
        app.MapGet("api/users/{userId:guid}", async (Guid userId, IQueryHandler<GetUser, UserDTO> handler) =>
        {
            var userDto = await handler.HandleAsync(new GetUser { UserId = userId });
            return userDto is null ? Results.NotFound() : Results.Ok(userDto);
        })
        .RequireAuthorization("is-admin");
        app.MapPost("api/users", async (SignUp command, ICommandHandler<SignUp> handler) =>
        {
            command = command with { UserId = Guid.NewGuid() };
            await handler.HandleAsync(command);
            return Results.CreatedAtRoute("me");
        });

        return app;
    }
}