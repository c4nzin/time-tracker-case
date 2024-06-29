using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using time_tracker_case;
using time_tracker_case.Services;

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

        Console.WriteLine(userId);

        if (user == null)
        {
            throw new BadHttpRequestException("You are not logged in.");
        }

        return _mapper.Map<AuthenticatedUserDto>(user);
    }
}
