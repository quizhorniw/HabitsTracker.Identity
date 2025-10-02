using SevSU.HabitsTracker.Identity.Api;
using SevSU.HabitsTracker.Identity.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

var group = app.MapGroup("/api");
app.MapEndpoints(group);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MigrateDb();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();