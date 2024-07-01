using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using time_tracker_case;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IMapper _mapper;

    public UserService(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper
    )
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<AuthenticatedUserDto> GetAuthenticatedUser()
    {
        var userId = _httpContextAccessor
            .HttpContext!.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)!
            .Value;
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new BadHttpRequestException("You are not logged in.");
        }

        return _mapper.Map<AuthenticatedUserDto>(user);
    }

    public async Task<AuthenticatedUserDto> UpdateAuthenticatedUser(UpdateUserDto updateUserDto)
    {
        var userId = _httpContextAccessor
            .HttpContext!.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)!
            .Value;
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new BadHttpRequestException("You are not logged in.");
        }

        user.UserName = updateUserDto.Username;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new Exception("User update failed.");
        }

        return _mapper.Map<AuthenticatedUserDto>(result);
    }
}
