namespace SevSU.HabitsTracker.Identity.Api.Time;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}