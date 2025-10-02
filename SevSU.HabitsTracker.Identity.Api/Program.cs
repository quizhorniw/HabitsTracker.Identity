using Microsoft.AspNetCore.Hosting.Builder;
using SevSU.HabitsTracker.Identity.Api;
using SevSU.HabitsTracker.Identity.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MigrateDb();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
