namespace time_tracker_case;

public class CreateTimeRecordDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double HourlyRate { get; set; }
}
