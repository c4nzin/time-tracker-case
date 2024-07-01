using System.ComponentModel.DataAnnotations;

namespace time_tracker_case;

public class UpdateUserDto
{
    [Required(ErrorMessage = "Username is required.")]
    public required string Username { get; set; }
}
