namespace SevSU.HabitsTracker.Identity.Api.Authentication;

public interface IUserContext
{
    Guid UserId { get; }
    
    bool IsInRole(string role);
}