using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardSnapshotDto>> GetSnapshot([FromQuery] DateTime? from, [FromQuery] DateTime? to, CancellationToken cancellationToken)
    {
        var result = await _dashboardService.GetSnapshotAsync(from, to, cancellationToken);
        return Ok(result);
    }
}
