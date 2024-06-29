using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace time_tracker_case.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerUserDto.Username);

            if (userExists != null)
            {
                return new BadRequestObjectResult("User is already registered.");
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerUserDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUserDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult("Check your credentials.");
            }

            return new OkObjectResult("User created.");
        }
    }
}
