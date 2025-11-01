using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Infrastructure.Repositories;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class ChangeRequestService : IChangeRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ChangeRequestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<ChangeRequestSummaryDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.ChangeRequests
            .Query()
            .Where(x => x.ProjectId == projectId)
            .OrderByDescending(x => x.RequestedOn)
            .ProjectTo<ChangeRequestSummaryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<ChangeRequestDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ChangeRequests.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ChangeRequestDetailDto>(entity);
    }

    public async Task<ChangeRequestDetailDto> CreateAsync(ChangeRequestUpsertRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new ChangeRequest
        {
            ProjectId = request.ProjectId,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            EstimatedEffortHours = request.EstimatedEffortHours,
            EstimatedCost = request.EstimatedCost,
            EstimatedRevenue = request.EstimatedRevenue,
            RequestedOn = request.RequestedOn,
            ApprovedOn = request.ApprovedOn,
            RequestedBy = request.RequestedBy,
            ApprovedBy = request.ApprovedBy
        };

        await _unitOfWork.ChangeRequests.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ChangeRequestDetailDto>(entity);
    }

    public async Task<ChangeRequestDetailDto?> UpdateAsync(Guid id, ChangeRequestUpsertRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ChangeRequests.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Status = request.Status;
        entity.EstimatedEffortHours = request.EstimatedEffortHours;
        entity.EstimatedCost = request.EstimatedCost;
        entity.EstimatedRevenue = request.EstimatedRevenue;
        entity.RequestedOn = request.RequestedOn;
        entity.ApprovedOn = request.ApprovedOn;
        entity.RequestedBy = request.RequestedBy;
        entity.ApprovedBy = request.ApprovedBy;
        entity.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.ChangeRequests.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ChangeRequestDetailDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ChangeRequests.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
