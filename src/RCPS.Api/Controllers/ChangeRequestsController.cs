using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/changerequests")]
public class ChangeRequestsController : ControllerBase
{
    private readonly IChangeRequestService _changeRequestService;

    public ChangeRequestsController(IChangeRequestService changeRequestService)
    {
        _changeRequestService = changeRequestService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ChangeRequestSummaryDto>>> GetAll(Guid projectId, CancellationToken cancellationToken)
    {
        var changeRequests = await _changeRequestService.GetByProjectAsync(projectId, cancellationToken);
        return Ok(changeRequests);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ChangeRequestDetailDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var changeRequest = await _changeRequestService.GetByIdAsync(id, cancellationToken);
        if (changeRequest is null)
        {
            return NotFound();
        }

        return Ok(changeRequest);
    }

    [HttpPost]
    public async Task<ActionResult<ChangeRequestDetailDto>> Create(
        Guid projectId,
        [FromBody] ChangeRequestUpsertRequest request,
        CancellationToken cancellationToken)
    {
        var payload = request with { ProjectId = projectId };
        var changeRequest = await _changeRequestService.CreateAsync(payload, cancellationToken);
        return CreatedAtAction(nameof(Get), new { projectId, id = changeRequest.Id }, changeRequest);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ChangeRequestDetailDto>> Update(
        Guid projectId,
        Guid id,
        [FromBody] ChangeRequestUpsertRequest request,
        CancellationToken cancellationToken)
    {
        var payload = request with { ProjectId = projectId };
        var changeRequest = await _changeRequestService.UpdateAsync(id, payload, cancellationToken);
        if (changeRequest is null)
        {
            return NotFound();
        }

        return Ok(changeRequest);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _changeRequestService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
