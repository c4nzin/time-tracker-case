using System.ComponentModel.DataAnnotations;

namespace time_tracker_case;

public class UpdateUserDto
{
    public required string Username { get; set; }
}
