using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IProjectService
{
    Task<IReadOnlyCollection<ProjectSummaryDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProjectDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProjectDetailDto> CreateAsync(ProjectUpsertRequest request, CancellationToken cancellationToken = default);
    Task<ProjectDetailDto?> UpdateAsync(Guid id, ProjectUpsertRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
