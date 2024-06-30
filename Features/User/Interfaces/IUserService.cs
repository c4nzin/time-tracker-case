namespace time_tracker_case;

public interface IUserService
{
    Task<AuthenticatedUserDto> GetAuthenticatedUser();
    Task<AuthenticatedUserDto> UpdateAuthenticatedUser(UpdateUserDto updateUserDto);
}
