using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Services.Interfaces;
using RCPS.Infrastructure.Repositories;

namespace RCPS.Services.Implementations;

public class ClientService : IClientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<ClientSummaryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Clients
            .Query()
            .OrderBy(x => x.Name)
            .ProjectTo<ClientSummaryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<ClientDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Clients
            .Query()
            .Include(x => x.Projects)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity is null ? null : _mapper.Map<ClientDetailDto>(entity);
    }

    public async Task<ClientDetailDto> CreateAsync(ClientUpsertRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new Client
        {
            Name = request.Name,
            PrimaryContactName = request.PrimaryContactName,
            PrimaryContactEmail = request.PrimaryContactEmail,
            PhoneNumber = request.PhoneNumber,
            BillingAddress = request.BillingAddress,
            ShippingAddress = request.ShippingAddress
        };

        await _unitOfWork.Clients.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ClientDetailDto>(entity);
    }

    public async Task<ClientDetailDto?> UpdateAsync(Guid id, ClientUpsertRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Clients.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Name = request.Name;
        entity.PrimaryContactName = request.PrimaryContactName;
        entity.PrimaryContactEmail = request.PrimaryContactEmail;
        entity.PhoneNumber = request.PhoneNumber;
        entity.BillingAddress = request.BillingAddress;
        entity.ShippingAddress = request.ShippingAddress;
        entity.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Clients.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ClientDetailDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.Clients.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
