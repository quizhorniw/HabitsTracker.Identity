using Microsoft.EntityFrameworkCore;
using SevSU.HabitsTracker.Identity.Api.DbContexts;

namespace SevSU.HabitsTracker.Identity.Api.Extensions;

public static class DatabaseExtensions
{
    public static async void MigrateDb(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}