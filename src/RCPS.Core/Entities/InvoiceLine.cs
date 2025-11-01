namespace RCPS.Core.Entities;

public class InvoiceLine : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }

    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal => Quantity * UnitPrice;

    public Guid? WorkLogId { get; set; }
    public WorkLog? WorkLog { get; set; }
}
