using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/invoices")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<InvoiceSummaryDto>>> GetAll(Guid projectId, CancellationToken cancellationToken)
    {
        var invoices = await _invoiceService.GetByProjectAsync(projectId, cancellationToken);
        return Ok(invoices);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<InvoiceDetailDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceService.GetByIdAsync(id, cancellationToken);
        if (invoice is null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceDetailDto>> Create(
        Guid projectId,
        [FromBody] InvoiceUpsertRequest request,
        CancellationToken cancellationToken)
    {
        var payload = request with { ProjectId = projectId };
        var invoice = await _invoiceService.CreateAsync(payload, cancellationToken);
        return CreatedAtAction(nameof(Get), new { projectId, id = invoice.Id }, invoice);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<InvoiceDetailDto>> Update(
        Guid projectId,
        Guid id,
        [FromBody] InvoiceUpsertRequest request,
        CancellationToken cancellationToken)
    {
        var payload = request with { ProjectId = projectId };
        var invoice = await _invoiceService.UpdateAsync(id, payload, cancellationToken);
        if (invoice is null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _invoiceService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
