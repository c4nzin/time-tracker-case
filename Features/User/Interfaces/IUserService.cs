using Microsoft.AspNetCore.Mvc;

namespace time_tracker_case;

public interface IUserService
{
    public Task<AuthenticatedUserDto> GetAuthenticatedUser();
}
