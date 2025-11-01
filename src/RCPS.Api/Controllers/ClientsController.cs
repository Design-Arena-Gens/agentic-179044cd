using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ClientSummaryDto>>> GetAll(CancellationToken cancellationToken)
    {
        var clients = await _clientService.GetAllAsync(cancellationToken);
        return Ok(clients);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDetailDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var client = await _clientService.GetByIdAsync(id, cancellationToken);
        if (client is null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDetailDto>> Create([FromBody] ClientUpsertRequest request, CancellationToken cancellationToken)
    {
        var client = await _clientService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientDetailDto>> Update(Guid id, [FromBody] ClientUpsertRequest request, CancellationToken cancellationToken)
    {
        var client = await _clientService.UpdateAsync(id, request, cancellationToken);
        if (client is null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _clientService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
