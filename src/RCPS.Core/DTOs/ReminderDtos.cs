using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record ReminderDto(
    Guid Id,
    ReminderType ReminderType,
    DateTime TriggerOn,
    bool IsAcknowledged,
    string? Message);

public record ReminderUpsertRequest(
    Guid ProjectId,
    ReminderType ReminderType,
    DateTime TriggerOn,
    bool IsAcknowledged,
    string? Message);
