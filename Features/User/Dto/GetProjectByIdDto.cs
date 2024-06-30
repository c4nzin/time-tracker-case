using System.ComponentModel.DataAnnotations;

namespace time_tracker_case;

public class GetProjectByIdDto
{
    [Required(ErrorMessage = "ProjectId is required.")]
    public Guid ProjectId { get; set; }
}
