using SevSU.HabitsTracker.Identity.Api.Models.Entities;

namespace SevSU.HabitsTracker.Identity.Api.Authentication;

public interface ITokenProvider
{
    string CreateAccessToken(User user);
    RefreshToken GenerateRefreshToken(Guid userId);
}