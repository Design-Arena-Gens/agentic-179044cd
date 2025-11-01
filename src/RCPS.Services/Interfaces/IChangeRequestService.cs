using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IChangeRequestService
{
    Task<IReadOnlyCollection<ChangeRequestSummaryDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<ChangeRequestDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ChangeRequestDetailDto> CreateAsync(ChangeRequestUpsertRequest request, CancellationToken cancellationToken = default);
    Task<ChangeRequestDetailDto?> UpdateAsync(Guid id, ChangeRequestUpsertRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
