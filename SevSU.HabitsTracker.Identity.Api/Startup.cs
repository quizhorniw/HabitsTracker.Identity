using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SevSU.HabitsTracker.Identity.Api.Authentication;
using SevSU.HabitsTracker.Identity.Api.DbContexts;
using SevSU.HabitsTracker.Identity.Api.Extensions;
using SevSU.HabitsTracker.Identity.Api.Repositories;
using SevSU.HabitsTracker.Identity.Api.Services;
using SevSU.HabitsTracker.Identity.Api.Time;

namespace SevSU.HabitsTracker.Identity.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureDatabase(services);

        services.AddEndpoints(Assembly.GetExecutingAssembly());

        services.AddEndpointsApiExplorer();
        services.AddOpenApi();

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUsersService, UsersService>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        AddAuthentication(services);
    }

    private void ConfigureDatabase(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opts => opts
            .UseNpgsql(_configuration.GetConnectionString("IdentityDB"))
            .UseSnakeCaseNamingConvention());
    }
    
    private void AddAuthentication(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!)),
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
        services.AddScoped<ICookieContext, CookieContext>();
    }
}