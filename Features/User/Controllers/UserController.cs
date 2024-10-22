using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace time_tracker_case.Controllers;

[Route("api/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
    }

    [HttpGet("me")]
    public async Task<AuthenticatedUserDto> GetAuthenticatedUser()
    {
        return await _userService.GetAuthenticatedUser();
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedUser = await _userService.UpdateAuthenticatedUser(updateUserDto);
        return Ok(updatedUser);
    }
}
