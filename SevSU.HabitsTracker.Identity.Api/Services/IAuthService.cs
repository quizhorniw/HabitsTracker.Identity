using SevSU.HabitsTracker.Identity.Api.Models.Dtos;

namespace SevSU.HabitsTracker.Identity.Api.Services;

public interface IAuthService
{
    Task<Guid> Register(RegisterRequestDto request, CancellationToken cancellationToken);
}