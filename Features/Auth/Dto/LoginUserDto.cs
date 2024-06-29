using System.ComponentModel.DataAnnotations;

namespace time_tracker_case;

public class LoginUserDto
{
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
}
