using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace time_tracker_case.Models;

public class TimeRecord
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column(TypeName = "uuid")]
    public Guid ProjectId { get; set; }

    [ForeignKey("ProjectId")]
    public Project Project { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double HourlyRate { get; set; }
    public double Profit { get; set; }
}
