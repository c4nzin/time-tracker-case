using Microsoft.EntityFrameworkCore;
using time_tracker_case.Contexts;
using time_tracker_case.Models;

//TODO : add mapper to whole project.

namespace time_tracker_case.Services;

public class TimeRecordService : ITimeRecordService
{
    private readonly IdentityDbContext _context;
    private readonly IUserService _userService;

    public TimeRecordService(IdentityDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<TimeRecord> CreateTimeRecord(
        CreateTimeRecordDto createTimeRecordDto,
        Guid projectId
    )
    {
        var authenticatedUser = await _userService.GetAuthenticatedUser();

        if (createTimeRecordDto.StartDate > createTimeRecordDto.EndDate)
        {
            throw new BadHttpRequestException("Invalid date.");
        }

        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
        if (project == null)
        {
            throw new UnauthorizedAccessException(
                "The Project not belong to the authenticated user or its just not exists"
            );
            // 6aae770b-6add-4ebf-9776-d9d8ff506ddd project id
        }

        var workedInMinutes = (
            createTimeRecordDto.EndDate - createTimeRecordDto.StartDate
        ).TotalMinutes;

        var hoursWorkedInHours = workedInMinutes / 60;
        var profit = createTimeRecordDto.HourlyRate * hoursWorkedInHours;

        var timeRecord = new TimeRecord
        {
            ProjectId = project.Id,
            StartDate = createTimeRecordDto.StartDate,
            EndDate = createTimeRecordDto.EndDate,
            HourlyRate = createTimeRecordDto.HourlyRate,
            Profit = profit
        };

        _context.TimeRecords.Add(timeRecord);
        await _context.SaveChangesAsync();

        return timeRecord;
    }

    public async Task<List<TimeRecord>> GetTimeRecords(Guid projectId)
    {
        var authenticatedUser = await _userService.GetAuthenticatedUser();

        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null)
        {
            throw new UnauthorizedAccessException("No project found.");
        }

        if (project.UserId != authenticatedUser.Id)
        {
            throw new UnauthorizedAccessException(
                "The Project not belong to the authenticated user"
            );
        }

        var timeRecords = await _context
            .TimeRecords.Where(table => table.ProjectId == projectId)
            .ToListAsync();

        return timeRecords;
    }
}
