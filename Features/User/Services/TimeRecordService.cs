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

    public async Task<TimeRecord> CreateTimeRecord(CreateTimeRecordDto createTimeRecordDto)
    {
        var authenticatedUser = await _userService.GetAuthenticatedUser();
        if (authenticatedUser == null)
        {
            throw new UnauthorizedAccessException("User not authenticated.");
        }

        if (createTimeRecordDto.StartDate > createTimeRecordDto.EndDate)
        {
            throw new BadHttpRequestException("Invalid date.");
        }

        var project = await _context.Projects.FirstOrDefaultAsync(p =>
            p.Id == authenticatedUser.Id
        );
        if (project == null || project.UserId != authenticatedUser.Id)
        {
            throw new UnauthorizedAccessException(
                "The Project not belong to the authenticated user or its just not exists"
            );
        }

        var hoursWorked = (
            createTimeRecordDto.EndDate - createTimeRecordDto.StartDate
        ).TotalMinutes;

        var hoursWorkedInHours = hoursWorked / 60;
        var profit = createTimeRecordDto.HourlyRate * hoursWorkedInHours;

        var timeRecord = new TimeRecord
        {
            Id = Guid.NewGuid(),
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
}
