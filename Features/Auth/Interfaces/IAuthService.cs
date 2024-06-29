using Microsoft.AspNetCore.Mvc;

namespace time_tracker_case;

public interface IAuthService
{
    Task<IActionResult> Register(RegisterUserDto registerUserDto);
}
