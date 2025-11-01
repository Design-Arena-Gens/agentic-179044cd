using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class Reminder : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public ReminderType ReminderType { get; set; } = ReminderType.InvoiceDue;
    public DateTime TriggerOn { get; set; }
    public bool IsAcknowledged { get; set; }
    public string? Message { get; set; }
}
