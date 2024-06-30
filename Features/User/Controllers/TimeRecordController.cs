using Microsoft.AspNetCore.Mvc;

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
        [FromBody] CreateTimeRecordDto createTimeRecordDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid body.");
        }
        // if (createTimeRecordDto == null) { }

        var timeRecord = await _timeRecordService.CreateTimeRecord(createTimeRecordDto);

        return Ok(timeRecord);
    }
}
