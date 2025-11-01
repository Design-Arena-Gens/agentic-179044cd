using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record StatementOfWorkSummaryDto(
    Guid Id,
    string Title,
    string Version,
    decimal ContractAmount,
    BillingFrequency BillingFrequency,
    DateTime EffectiveDate,
    DateTime? ExpiryDate);

public record StatementOfWorkDetailDto(
    Guid Id,
    Guid ProjectId,
    string Title,
    string Version,
    decimal ContractAmount,
    BillingFrequency BillingFrequency,
    DateTime EffectiveDate,
    DateTime? ExpiryDate,
    string? ScopeOverview,
    IReadOnlyCollection<SowMilestoneDto> Milestones);

public record StatementOfWorkUpsertRequest(
    Guid ProjectId,
    string Title,
    string Version,
    decimal ContractAmount,
    BillingFrequency BillingFrequency,
    DateTime EffectiveDate,
    DateTime? ExpiryDate,
    string? ScopeOverview,
    IReadOnlyCollection<SowMilestoneUpsertRequest> Milestones);

public record SowMilestoneDto(
    Guid Id,
    string Name,
    decimal Amount,
    DateTime PlannedDate,
    DateTime? ActualDate,
    bool IsCompleted);

public record SowMilestoneUpsertRequest(
    string Name,
    decimal Amount,
    DateTime PlannedDate,
    DateTime? ActualDate,
    bool IsCompleted);
