using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IClientService
{
    Task<IReadOnlyCollection<ClientSummaryDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ClientDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ClientDetailDto> CreateAsync(ClientUpsertRequest request, CancellationToken cancellationToken = default);
    Task<ClientDetailDto?> UpdateAsync(Guid id, ClientUpsertRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
