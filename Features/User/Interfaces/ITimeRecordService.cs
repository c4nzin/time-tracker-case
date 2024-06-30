using time_tracker_case.Models;

namespace time_tracker_case;

public interface ITimeRecordService
{
    Task<TimeRecord> CreateTimeRecord(CreateTimeRecordDto createTimeRecordDto);
}
