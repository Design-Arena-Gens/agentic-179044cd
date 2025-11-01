using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class SowMilestone : BaseEntity
{
    public Guid StatementOfWorkId { get; set; }
    public StatementOfWork? StatementOfWork { get; set; }

    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime PlannedDate { get; set; }
    public DateTime? ActualDate { get; set; }
    public bool IsCompleted { get; set; }
    public BillingFrequency BillingType { get; set; } = BillingFrequency.Milestone;
}
