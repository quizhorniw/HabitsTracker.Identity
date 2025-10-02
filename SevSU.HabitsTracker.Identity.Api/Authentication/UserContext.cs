using System.Security.Claims;

namespace SevSU.HabitsTracker.Identity.Api.Authentication;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal User =>
        _httpContextAccessor.HttpContext?.User 
        ?? throw new ApplicationException("User context is unavailable");

    public Guid UserId => User.GetUserId();

    public bool IsInRole(string role) => User.IsInRole(role);
}