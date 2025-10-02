using System.Security.Authentication;
using SevSU.HabitsTracker.Identity.Api.Authentication;
using SevSU.HabitsTracker.Identity.Api.Models.Dtos;
using SevSU.HabitsTracker.Identity.Api.Models.Entities;
using SevSU.HabitsTracker.Identity.Api.Repositories;

namespace SevSU.HabitsTracker.Identity.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly ICookieContext _cookieContext;
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    
    public AuthService(IUsersRepository usersRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider, 
        ICookieContext cookieContext, IRefreshTokensRepository refreshTokensRepository)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _cookieContext = cookieContext;
        _refreshTokensRepository = refreshTokensRepository;
    }

    public async Task<Guid> Register(RegisterRequestDto registerRequestDto, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = registerRequestDto.Email,
            Username = registerRequestDto.Username,
            PasswordHash = _passwordHasher.Hash(registerRequestDto.Password),
        };

        await _usersRepository.AddAsync(user, cancellationToken);
        
        return user.Id;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            throw new AuthenticationException("No user found");
        }
        
        var verified = _passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!verified)
        {
            throw new AuthenticationException("Invalid password");
        }
        
        var accessToken = _tokenProvider.CreateAccessToken(user);
        
        var refreshToken = _tokenProvider.GenerateRefreshToken(user.Id);
        _cookieContext.AppendRefreshTokenCookie(refreshToken);
        await _refreshTokensRepository.AddAsync(refreshToken, cancellationToken);
        
        return new LoginResponseDto(accessToken);
    }
}