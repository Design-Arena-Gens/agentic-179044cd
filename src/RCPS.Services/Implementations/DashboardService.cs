using Microsoft.EntityFrameworkCore;
using RCPS.Core.DTOs;
using RCPS.Infrastructure.Repositories;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DashboardSnapshotDto> GetSnapshotAsync(DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
    {
        var startDate = from ?? DateTime.UtcNow.AddMonths(-6);
        var endDate = to ?? DateTime.UtcNow;

        var projectsQuery = _unitOfWork.Projects.Query().Where(x => x.StartDate <= endDate);
        var filteredWorkLogs = _unitOfWork.WorkLogs.Query().Where(x => x.WorkDate >= startDate && x.WorkDate <= endDate);
        var invoicesQuery = _unitOfWork.Invoices.Query().Where(x => x.IssueDate >= startDate && x.IssueDate <= endDate);

        var totalRecognizedRevenue = await projectsQuery.SumAsync(x => x.RecognizedRevenue, cancellationToken);
        var totalCost = await projectsQuery.SumAsync(x => x.ActualCost, cancellationToken);
        var totalBilled = await projectsQuery.SumAsync(x => x.BilledAmount, cancellationToken);
        var grossMarginPercentage = totalRecognizedRevenue == 0 ? 0 : decimal.Round((totalRecognizedRevenue - totalCost) / totalRecognizedRevenue * 100, 2);

        var trend = await filteredWorkLogs
            .GroupBy(x => new { x.WorkDate.Year, x.WorkDate.Month })
            .Select(g => new ProfitabilityTrendPoint(
                new DateTime(g.Key.Year, g.Key.Month, 1),
                g.Sum(w => w.IsBillable ? w.Hours * w.BillableRate : 0),
                g.Sum(w => w.IsBillable ? w.Hours * w.BillableRate * 0.6m : 0),
                g.Sum(w => w.IsBillable ? w.Hours * w.BillableRate * 0.4m : 0)))
            .OrderBy(x => x.Period)
            .ToListAsync(cancellationToken);

        var roleAllocations = await _unitOfWork.ProjectRoles
            .Query()
            .GroupBy(x => new { x.UserProfileId, x.UserProfile!.FullName })
            .Select(g => new
            {
                g.Key.UserProfileId,
                g.Key.FullName,
                Allocated = g.Sum(x => x.AllocatedHours)
            })
            .ToListAsync(cancellationToken);

        var workLogHours = await filteredWorkLogs
            .GroupBy(x => x.UserProfileId)
            .Select(g => new
            {
                g.Key,
                Hours = g.Sum(x => x.Hours)
            })
            .ToListAsync(cancellationToken);

        var utilization = roleAllocations
            .Select(r =>
            {
                var utilized = workLogHours.FirstOrDefault(x => x.Key == r.UserProfileId)?.Hours ?? 0;
                return new ResourceUtilizationPoint(r.FullName, r.Allocated, utilized);
            })
            .ToList();

        foreach (var orphan in workLogHours)
        {
            if (roleAllocations.All(r => r.UserProfileId != orphan.Key))
            {
                utilization.Add(new ResourceUtilizationPoint($"Resource {orphan.Key.ToString()[..8]}", 0, orphan.Hours));
            }
        }

        var aging = await invoicesQuery
            .Select(invoice => new
            {
                invoice.TotalAmount,
                invoice.AmountPaid,
                invoice.DueDate
            })
            .AsEnumerable()
            .Select(x => new
            {
                Outstanding = x.TotalAmount - x.AmountPaid,
                Age = (DateTime.UtcNow.Date - x.DueDate.Date).Days
            })
            .GroupBy(x => GetAgingBucketLabel(x.Age))
            .Select(g => new AgingBucket(g.Key, g.Sum(x => x.Outstanding)))
            .ToList();

        return new DashboardSnapshotDto(
            TotalRecognizedRevenue: totalRecognizedRevenue,
            TotalCost: totalCost,
            TotalBilled: totalBilled,
            GrossMarginPercentage: grossMarginPercentage,
            ProfitabilityTrend: trend,
            ResourceUtilization: utilization,
            InvoiceAging: aging);
    }

    private static string GetAgingBucketLabel(int ageDays)
    {
        if (ageDays <= 0)
        {
            return "Current";
        }

        if (ageDays <= 30)
        {
            return "1-30";
        }

        if (ageDays <= 60)
        {
            return "31-60";
        }

        if (ageDays <= 90)
        {
            return "61-90";
        }

        return "90+";
    }
}
