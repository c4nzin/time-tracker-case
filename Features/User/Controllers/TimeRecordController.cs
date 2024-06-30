using Microsoft.AspNetCore.Mvc;
using time_tracker_case.Models;

namespace time_tracker_case.Controllers;

[Route("api/projects/{projectId}/records")]
[ApiController]
public class TimeRecordController : ControllerBase
{
    private readonly ITimeRecordService _timeRecordService;

    public TimeRecordController(ITimeRecordService timeRecordService)
    {
        _timeRecordService = timeRecordService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTimeRecord(
        [FromBody] CreateTimeRecordDto createTimeRecordDto,
        Guid projectId
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid body.");
        }

        var timeRecord = await _timeRecordService.CreateTimeRecord(createTimeRecordDto, projectId);

        return Ok(timeRecord);
    }

    [HttpGet]
    public async Task<IActionResult> GetTimeRecords([FromQuery] Guid projectId)
    {
        var getAllTimeRecords = await _timeRecordService.GetTimeRecords(projectId);

        return Ok(getAllTimeRecords);
    }
}
