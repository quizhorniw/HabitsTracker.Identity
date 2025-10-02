using SevSU.HabitsTracker.Identity.Api.Models.Dtos;
using SevSU.HabitsTracker.Identity.Api.Repositories;

namespace SevSU.HabitsTracker.Identity.Api.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<UserDto?> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _usersRepository.GetByIdAsync(id, cancellationToken);
        return user is null
            ? null
            : new UserDto(user.Id, user.Email, user.Username);
    }
}