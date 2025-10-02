using SevSU.HabitsTracker.Identity.Api.Models.Dtos;
using SevSU.HabitsTracker.Identity.Api.Services;

namespace SevSU.HabitsTracker.Identity.Api.Endpoints.Authentication;

internal sealed class Login : IEndpoint
{
    internal sealed record Request(string Email, string Password);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login",
                async (Request request, IAuthService authService, CancellationToken cancellationToken) =>
                {
                    var dto = new LoginRequestDto(request.Email, request.Password);
                    return await authService.Login(dto, cancellationToken);
                })
            .AllowAnonymous();
    }
}