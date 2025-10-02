namespace SevSU.HabitsTracker.Identity.Api.Authentication;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}