using SevSU.HabitsTracker.Identity.Api.Models.Dtos;
using SevSU.HabitsTracker.Identity.Api.Services;

namespace SevSU.HabitsTracker.Identity.Api.Endpoints.Authentication;

internal sealed class Register : IEndpoint
{
    internal sealed record Request(
        string Email,
        string Username,
        string Password
    );
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", 
                async (Request request, IAuthService authService, CancellationToken cancellationToken) => 
                { 
                    var dto = new RegisterRequestDto(request.Email, request.Username, request.Password); 
                    return await authService.Register(dto, cancellationToken); 
                })
            .AllowAnonymous();
    }
}