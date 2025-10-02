using SevSU.HabitsTracker.Identity.Api.Authentication;
using SevSU.HabitsTracker.Identity.Api.Models.Dtos;
using SevSU.HabitsTracker.Identity.Api.Models.Entities;
using SevSU.HabitsTracker.Identity.Api.Repositories;

namespace SevSU.HabitsTracker.Identity.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IUsersRepository usersRepository, IPasswordHasher passwordHasher)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
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
}