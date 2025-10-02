using SevSU.HabitsTracker.Identity.Api.Models.Dtos;

namespace SevSU.HabitsTracker.Identity.Api.Services;

public interface IUsersService
{
    Task<UserDto?> GetUserById(Guid id, CancellationToken cancellationToken = default);
}