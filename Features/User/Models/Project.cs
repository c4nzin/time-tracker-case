using System.ComponentModel.DataAnnotations;

namespace time_tracker_case.Models;

public class Project
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public Guid UserId { get; set; }
}
