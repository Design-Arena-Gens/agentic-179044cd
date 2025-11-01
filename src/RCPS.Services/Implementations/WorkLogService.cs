using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Infrastructure.Repositories;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class WorkLogService : IWorkLogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WorkLogService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<WorkLogSummaryDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.WorkLogs
            .Query()
            .Where(x => x.ProjectId == projectId)
            .OrderByDescending(x => x.WorkDate)
            .ProjectTo<WorkLogSummaryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<WorkLogDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.WorkLogs.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<WorkLogDetailDto>(entity);
    }

    public async Task<WorkLogDetailDto> CreateAsync(WorkLogUpsertRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new WorkLog
        {
            ProjectId = request.ProjectId,
            StatementOfWorkId = request.StatementOfWorkId,
            ChangeRequestId = request.ChangeRequestId,
            UserProfileId = request.UserProfileId,
            WorkDate = request.WorkDate,
            Hours = request.Hours,
            BillableRate = request.BillableRate,
            IsBillable = request.IsBillable,
            Notes = request.Notes
        };

        await _unitOfWork.WorkLogs.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<WorkLogDetailDto>(entity);
    }

    public async Task<WorkLogDetailDto?> UpdateAsync(Guid id, WorkLogUpsertRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.WorkLogs.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.ProjectId = request.ProjectId;
        entity.StatementOfWorkId = request.StatementOfWorkId;
        entity.ChangeRequestId = request.ChangeRequestId;
        entity.UserProfileId = request.UserProfileId;
        entity.WorkDate = request.WorkDate;
        entity.Hours = request.Hours;
        entity.BillableRate = request.BillableRate;
        entity.IsBillable = request.IsBillable;
        entity.Notes = request.Notes;
        entity.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.WorkLogs.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<WorkLogDetailDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.WorkLogs.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
