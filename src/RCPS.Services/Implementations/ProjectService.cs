using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Services.Interfaces;
using RCPS.Infrastructure.Repositories;

namespace RCPS.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<ProjectSummaryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Projects
            .Query()
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<ProjectSummaryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProjectDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Projects
            .Query()
            .Include(x => x.Client)
            .Include(x => x.StatementsOfWork).ThenInclude(x => x.Milestones)
            .Include(x => x.ChangeRequests)
            .Include(x => x.WorkLogs)
            .Include(x => x.Invoices).ThenInclude(x => x.Lines)
            .Include(x => x.Reminders)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity is null ? null : _mapper.Map<ProjectDetailDto>(entity);
    }

    public async Task<ProjectDetailDto> CreateAsync(ProjectUpsertRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new Project
        {
            Name = request.Name,
            Code = request.Code,
            ClientId = request.ClientId,
            Status = request.Status,
            BudgetAmount = request.BudgetAmount,
            ActualCost = request.ActualCost,
            RecognizedRevenue = request.RecognizedRevenue,
            BilledAmount = request.BilledAmount,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Description = request.Description,
            EngagementLead = request.EngagementLead
        };

        await _unitOfWork.Projects.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(entity.Id, cancellationToken) ?? _mapper.Map<ProjectDetailDto>(entity);
    }

    public async Task<ProjectDetailDto?> UpdateAsync(Guid id, ProjectUpsertRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Projects.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Name = request.Name;
        entity.Code = request.Code;
        entity.ClientId = request.ClientId;
        entity.Status = request.Status;
        entity.BudgetAmount = request.BudgetAmount;
        entity.ActualCost = request.ActualCost;
        entity.RecognizedRevenue = request.RecognizedRevenue;
        entity.BilledAmount = request.BilledAmount;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.Description = request.Description;
        entity.EngagementLead = request.EngagementLead;
        entity.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Projects.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(entity.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.Projects.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
