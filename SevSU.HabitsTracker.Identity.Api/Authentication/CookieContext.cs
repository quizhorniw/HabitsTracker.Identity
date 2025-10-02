using SevSU.HabitsTracker.Identity.Api.Models.Entities;

namespace SevSU.HabitsTracker.Identity.Api.Authentication;

public class CookieContext : ICookieContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void AppendRefreshTokenCookie(RefreshToken refreshToken)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = refreshToken.ExpiresAt
            });
    }

    public void DeleteRefreshTokenCookie()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");
    }
}