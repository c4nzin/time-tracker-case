using System.ComponentModel.DataAnnotations;

namespace time_tracker_case;

public class CreateProjectDto
{
    [Required(ErrorMessage = "Project name is required.")]
    public string Name { get; set; }
}
