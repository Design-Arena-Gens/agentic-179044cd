using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record ProjectSummaryDto(
    Guid Id,
    string Name,
    string? Code,
    Guid ClientId,
    ProjectStatus Status,
    decimal BudgetAmount,
    decimal ActualCost,
    decimal RecognizedRevenue,
    DateTime StartDate,
    DateTime? EndDate);

public record ProjectDetailDto(
    Guid Id,
    string Name,
    string? Code,
    ProjectStatus Status,
    decimal BudgetAmount,
    decimal ActualCost,
    decimal RecognizedRevenue,
    decimal BilledAmount,
    DateTime StartDate,
    DateTime? EndDate,
    string? Description,
    string? EngagementLead,
    ClientSummaryDto Client,
    IReadOnlyCollection<StatementOfWorkSummaryDto> StatementsOfWork,
    IReadOnlyCollection<ChangeRequestSummaryDto> ChangeRequests,
    IReadOnlyCollection<WorkLogSummaryDto> WorkLogs,
    IReadOnlyCollection<InvoiceSummaryDto> Invoices,
    IReadOnlyCollection<ReminderDto> Reminders);

public record ProjectUpsertRequest(
    string Name,
    string? Code,
    Guid ClientId,
    ProjectStatus Status,
    decimal BudgetAmount,
    decimal ActualCost,
    decimal RecognizedRevenue,
    decimal BilledAmount,
    DateTime StartDate,
    DateTime? EndDate,
    string? Description,
    string? EngagementLead);
