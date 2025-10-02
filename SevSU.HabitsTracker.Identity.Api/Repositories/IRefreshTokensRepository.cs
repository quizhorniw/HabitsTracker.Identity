using SevSU.HabitsTracker.Identity.Api.Models.Entities;

namespace SevSU.HabitsTracker.Identity.Api.Repositories;

public interface IRefreshTokensRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
}