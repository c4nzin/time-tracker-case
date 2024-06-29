using System;

namespace time_tracker_case.Models
{
    public class TimeRecord
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double HourlyRate { get; set; }
        public double Profit { get; set; }
    }
}
