using SevSU.HabitsTracker.Identity.Api.Models.Dtos;
using SevSU.HabitsTracker.Identity.Api.Services;

namespace SevSU.HabitsTracker.Identity.Api.Endpoints.Authentication;

internal sealed class Refresh : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/refresh", async (HttpRequest request, IAuthService authService, CancellationToken cancellationToken) =>
            {
                if (!request.Cookies.TryGetValue("refreshToken", out var oldToken))
                {
                    return Results.Unauthorized();
                }

                var dto = new RefreshRequestDto(oldToken);
                var result = await authService.Refresh(dto, cancellationToken);
                return Results.Ok(result);
            })
            .AllowAnonymous();
    }
}