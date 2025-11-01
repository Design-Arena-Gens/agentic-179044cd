using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class ChangeRequest : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ChangeRequestStatus Status { get; set; } = ChangeRequestStatus.Draft;
    public decimal EstimatedEffortHours { get; set; }
    public decimal EstimatedCost { get; set; }
    public decimal EstimatedRevenue { get; set; }
    public DateTime RequestedOn { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovedOn { get; set; }
    public string? RequestedBy { get; set; }
    public string? ApprovedBy { get; set; }
}
