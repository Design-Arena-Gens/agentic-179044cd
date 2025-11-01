using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/worklogs")]
public class WorkLogsController : ControllerBase
{
    private readonly IWorkLogService _workLogService;

    public WorkLogsController(IWorkLogService workLogService)
    {
        _workLogService = workLogService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<WorkLogSummaryDto>>> GetAll(Guid projectId, CancellationToken cancellationToken)
    {
        var workLogs = await _workLogService.GetByProjectAsync(projectId, cancellationToken);
        return Ok(workLogs);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorkLogDetailDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var workLog = await _workLogService.GetByIdAsync(id, cancellationToken);
        if (workLog is null)
        {
            return NotFound();
        }

        return Ok(workLog);
    }

    [HttpPost]
    public async Task<ActionResult<WorkLogDetailDto>> Create(
        Guid projectId,
        [FromBody] WorkLogUpsertRequest request,
        CancellationToken cancellationToken)
    {
        var payload = request with { ProjectId = projectId };
        var workLog = await _workLogService.CreateAsync(payload, cancellationToken);
        return CreatedAtAction(nameof(Get), new { projectId, id = workLog.Id }, workLog);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<WorkLogDetailDto>> Update(
        Guid projectId,
        Guid id,
        [FromBody] WorkLogUpsertRequest request,
        CancellationToken cancellationToken)
    {
        var payload = request with { ProjectId = projectId };
        var workLog = await _workLogService.UpdateAsync(id, payload, cancellationToken);
        if (workLog is null)
        {
            return NotFound();
        }

        return Ok(workLog);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _workLogService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
