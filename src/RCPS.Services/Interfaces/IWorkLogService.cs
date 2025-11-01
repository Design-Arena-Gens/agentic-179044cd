using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IWorkLogService
{
    Task<IReadOnlyCollection<WorkLogSummaryDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<WorkLogDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<WorkLogDetailDto> CreateAsync(WorkLogUpsertRequest request, CancellationToken cancellationToken = default);
    Task<WorkLogDetailDto?> UpdateAsync(Guid id, WorkLogUpsertRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
