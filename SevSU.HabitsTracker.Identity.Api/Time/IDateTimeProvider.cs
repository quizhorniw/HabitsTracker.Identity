namespace SevSU.HabitsTracker.Identity.Api.Time;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}