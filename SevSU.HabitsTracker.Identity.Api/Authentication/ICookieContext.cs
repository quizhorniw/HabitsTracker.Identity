using SevSU.HabitsTracker.Identity.Api.Models.Entities;

namespace SevSU.HabitsTracker.Identity.Api.Authentication;

public interface ICookieContext
{
    void AppendRefreshTokenCookie(RefreshToken refreshToken);
    void DeleteRefreshTokenCookie();
}