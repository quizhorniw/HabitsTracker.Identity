using SevSU.HabitsTracker.Identity.Api.Services;

namespace SevSU.HabitsTracker.Identity.Api.Endpoints.Users;

internal sealed class GetById : IEndpoint
{
    internal const string EndpointName = "GetUserById"; 
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:guid}", 
                async (Guid id, IUsersService usersService, CancellationToken cancellationToken) => 
                { 
                    var result = await usersService.GetUserById(id, cancellationToken); 
                    return result is not null ? Results.Ok(result) : Results.NotFound(); 
                })
            .WithName(EndpointName)
            .RequireAuthorization();
    }
}