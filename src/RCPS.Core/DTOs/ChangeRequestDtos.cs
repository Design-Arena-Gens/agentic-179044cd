using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record ChangeRequestSummaryDto(
    Guid Id,
    string Title,
    ChangeRequestStatus Status,
    decimal EstimatedEffortHours,
    decimal EstimatedCost,
    decimal EstimatedRevenue,
    DateTime RequestedOn);

public record ChangeRequestDetailDto(
    Guid Id,
    Guid ProjectId,
    string Title,
    string? Description,
    ChangeRequestStatus Status,
    decimal EstimatedEffortHours,
    decimal EstimatedCost,
    decimal EstimatedRevenue,
    DateTime RequestedOn,
    DateTime? ApprovedOn,
    string? RequestedBy,
    string? ApprovedBy);

public record ChangeRequestUpsertRequest(
    Guid ProjectId,
    string Title,
    string? Description,
    ChangeRequestStatus Status,
    decimal EstimatedEffortHours,
    decimal EstimatedCost,
    decimal EstimatedRevenue,
    DateTime RequestedOn,
    DateTime? ApprovedOn,
    string? RequestedBy,
    string? ApprovedBy);
