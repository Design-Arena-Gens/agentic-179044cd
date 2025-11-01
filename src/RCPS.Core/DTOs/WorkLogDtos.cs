namespace RCPS.Core.DTOs;

public record WorkLogSummaryDto(
    Guid Id,
    Guid UserProfileId,
    DateTime WorkDate,
    decimal Hours,
    decimal BillableRate,
    bool IsBillable,
    string? Notes);

public record WorkLogDetailDto(
    Guid Id,
    Guid ProjectId,
    Guid? StatementOfWorkId,
    Guid? ChangeRequestId,
    Guid UserProfileId,
    DateTime WorkDate,
    decimal Hours,
    decimal BillableRate,
    bool IsBillable,
    string? Notes);

public record WorkLogUpsertRequest(
    Guid ProjectId,
    Guid? StatementOfWorkId,
    Guid? ChangeRequestId,
    Guid UserProfileId,
    DateTime WorkDate,
    decimal Hours,
    decimal BillableRate,
    bool IsBillable,
    string? Notes);
