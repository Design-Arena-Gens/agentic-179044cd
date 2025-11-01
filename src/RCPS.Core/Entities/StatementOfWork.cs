using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class StatementOfWork : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Version { get; set; } = "1.0";
    public decimal ContractAmount { get; set; }
    public BillingFrequency BillingFrequency { get; set; } = BillingFrequency.Milestone;
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }

    public string? ScopeOverview { get; set; }

    public ICollection<SowMilestone> Milestones { get; set; } = new List<SowMilestone>();
}
