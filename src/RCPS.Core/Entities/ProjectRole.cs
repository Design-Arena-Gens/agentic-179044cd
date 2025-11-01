namespace RCPS.Core.Entities;

public class ProjectRole : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public Guid UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }

    public string RoleName { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public decimal AllocatedHours { get; set; }
}
