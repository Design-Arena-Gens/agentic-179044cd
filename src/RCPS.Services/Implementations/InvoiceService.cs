using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;
using RCPS.Infrastructure.Repositories;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class InvoiceService : IInvoiceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<InvoiceSummaryDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Invoices
            .Query()
            .Where(x => x.ProjectId == projectId)
            .OrderByDescending(x => x.IssueDate)
            .ProjectTo<InvoiceSummaryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<InvoiceDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Invoices
            .Query()
            .Include(x => x.Lines)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity is null ? null : _mapper.Map<InvoiceDetailDto>(entity);
    }

    public async Task<InvoiceDetailDto> CreateAsync(InvoiceUpsertRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new Invoice
        {
            ProjectId = request.ProjectId,
            InvoiceNumber = request.InvoiceNumber,
            IssueDate = request.IssueDate,
            DueDate = request.DueDate,
            Status = request.Status,
            Subtotal = request.Subtotal,
            TaxAmount = request.TaxAmount,
            TotalAmount = request.TotalAmount,
            AmountPaid = request.AmountPaid
        };

        foreach (var line in request.Lines)
        {
            entity.Lines.Add(new InvoiceLine
            {
                Description = line.Description,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice,
                WorkLogId = line.WorkLogId
            });
        }

        await _unitOfWork.Invoices.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(entity.Id, cancellationToken) ?? _mapper.Map<InvoiceDetailDto>(entity);
    }

    public async Task<InvoiceDetailDto?> UpdateAsync(Guid id, InvoiceUpsertRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Invoices
            .Query()
            .Include(x => x.Lines)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        entity.InvoiceNumber = request.InvoiceNumber;
        entity.IssueDate = request.IssueDate;
        entity.DueDate = request.DueDate;
        entity.Status = request.Status;
        entity.Subtotal = request.Subtotal;
        entity.TaxAmount = request.TaxAmount;
        entity.TotalAmount = request.TotalAmount;
        entity.AmountPaid = request.AmountPaid;
        entity.UpdatedAt = DateTime.UtcNow;

        entity.Lines.Clear();
        foreach (var line in request.Lines)
        {
            entity.Lines.Add(new InvoiceLine
            {
                Description = line.Description,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice,
                WorkLogId = line.WorkLogId
            });
        }

        await _unitOfWork.Invoices.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(entity.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.Invoices.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
