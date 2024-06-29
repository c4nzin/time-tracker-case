using System.ComponentModel.DataAnnotations;

namespace time_tracker_case;

public class AuthenticatedUserDto
{
    public string Username { get; }

    public string Email { get; }

    public Guid Id { get; }
}
