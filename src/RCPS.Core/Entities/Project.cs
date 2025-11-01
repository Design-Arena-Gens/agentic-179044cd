using System.ComponentModel.DataAnnotations;
using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class Project : BaseEntity
{
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Code { get; set; }

    public Guid ClientId { get; set; }
    public Client? Client { get; set; }

    public ProjectStatus Status { get; set; } = ProjectStatus.Draft;

    public decimal BudgetAmount { get; set; }
    public decimal ActualCost { get; set; }
    public decimal RecognizedRevenue { get; set; }
    public decimal BilledAmount { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public string? Description { get; set; }
    public string? EngagementLead { get; set; }

    public ICollection<ProjectRole> Roles { get; set; } = new List<ProjectRole>();
    public ICollection<StatementOfWork> StatementsOfWork { get; set; } = new List<StatementOfWork>();
    public ICollection<ChangeRequest> ChangeRequests { get; set; } = new List<ChangeRequest>();
    public ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
}
