using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardSnapshotDto> GetSnapshotAsync(DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
}
