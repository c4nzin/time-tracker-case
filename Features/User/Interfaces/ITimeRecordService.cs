using time_tracker_case.Models;

namespace time_tracker_case;

public interface ITimeRecordService
{
    Task<TimeRecord> CreateTimeRecord(CreateTimeRecordDto createTimeRecordDto, Guid projectId);
    Task<List<TimeRecord>> GetTimeRecords(Guid projectId);
    Task<List<TimeRecord>> FilterTimeRecords(
        FilterTimeRecordDto filterTimeRecordDto,
        Guid projectId
    );
}
