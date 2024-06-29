using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace time_tracker_case.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        var userExists = await _userManager.FindByNameAsync(registerUserDto.Username);

        if (userExists != null)
        {
            return new BadRequestObjectResult("User is already registered.");
        }

        var user = new ApplicationUser()
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

    public async Task<IActionResult> Login(LoginUserDto loginUserDto)
    {
        var user = await _userManager.FindByNameAsync(loginUserDto.Username);

        if (user is null)
        {
            return new BadRequestObjectResult("Check your credentials.");
        }

        if (await _userManager.CheckPasswordAsync(user, loginUserDto.Password))
        {
            var authClaims = new List<Claim> { new Claim(ClaimTypes.Sid, user.Id), };

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new OkObjectResult(
                new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                }
            );
        }
        throw new UnauthorizedAccessException("Unauthorized");
    }
}
