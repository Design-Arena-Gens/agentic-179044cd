using System.ComponentModel.DataAnnotations;

namespace RCPS.Core.Entities;

public class Client : BaseEntity
{
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? PrimaryContactName { get; set; }

    [MaxLength(200)]
    public string? PrimaryContactEmail { get; set; }

    [MaxLength(100)]
    public string? PhoneNumber { get; set; }

    public string? BillingAddress { get; set; }
    public string? ShippingAddress { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
