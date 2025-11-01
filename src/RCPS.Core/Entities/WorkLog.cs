namespace RCPS.Core.Entities;

public class WorkLog : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public Guid? StatementOfWorkId { get; set; }
    public StatementOfWork? StatementOfWork { get; set; }

    public Guid? ChangeRequestId { get; set; }
    public ChangeRequest? ChangeRequest { get; set; }

    public Guid UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }

    public DateTime WorkDate { get; set; }
    public decimal Hours { get; set; }
    public decimal BillableRate { get; set; }
    public bool IsBillable { get; set; } = true;
    public string? Notes { get; set; }
}
