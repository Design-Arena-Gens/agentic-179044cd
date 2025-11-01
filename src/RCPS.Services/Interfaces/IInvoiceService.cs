using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IInvoiceService
{
    Task<IReadOnlyCollection<InvoiceSummaryDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<InvoiceDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<InvoiceDetailDto> CreateAsync(InvoiceUpsertRequest request, CancellationToken cancellationToken = default);
    Task<InvoiceDetailDto?> UpdateAsync(Guid id, InvoiceUpsertRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
