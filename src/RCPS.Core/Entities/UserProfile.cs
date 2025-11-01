using System.ComponentModel.DataAnnotations;

namespace RCPS.Core.Entities;

public class UserProfile : BaseEntity
{
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Role { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Department { get; set; } = string.Empty;

    public decimal DefaultHourlyRate { get; set; }

    public ICollection<ProjectRole> ProjectRoles { get; set; } = new List<ProjectRole>();
    public ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
}
