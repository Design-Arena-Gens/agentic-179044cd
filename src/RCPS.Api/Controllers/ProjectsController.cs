using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ProjectSummaryDto>>> GetAll(CancellationToken cancellationToken)
    {
        var projects = await _projectService.GetAllAsync(cancellationToken);
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectDetailDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var project = await _projectService.GetByIdAsync(id, cancellationToken);
        if (project is null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDetailDto>> Create([FromBody] ProjectUpsertRequest request, CancellationToken cancellationToken)
    {
        var project = await _projectService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProjectDetailDto>> Update(Guid id, [FromBody] ProjectUpsertRequest request, CancellationToken cancellationToken)
    {
        var project = await _projectService.UpdateAsync(id, request, cancellationToken);
        if (project is null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _projectService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
