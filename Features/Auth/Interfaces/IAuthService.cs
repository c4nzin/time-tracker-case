using Microsoft.AspNetCore.Mvc;

namespace time_tracker_case;

public interface IAuthService
{
    public Task<IActionResult> Register(RegisterUserDto registerUserDto);
    public Task<IActionResult> Login(LoginUserDto loginUserDto);
}
