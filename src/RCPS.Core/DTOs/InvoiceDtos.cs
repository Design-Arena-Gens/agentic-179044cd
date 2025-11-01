using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record InvoiceSummaryDto(
    Guid Id,
    string InvoiceNumber,
    DateTime IssueDate,
    DateTime DueDate,
    InvoiceStatus Status,
    decimal TotalAmount,
    decimal AmountPaid);

public record InvoiceDetailDto(
    Guid Id,
    Guid ProjectId,
    string InvoiceNumber,
    DateTime IssueDate,
    DateTime DueDate,
    InvoiceStatus Status,
    decimal Subtotal,
    decimal TaxAmount,
    decimal TotalAmount,
    decimal AmountPaid,
    IReadOnlyCollection<InvoiceLineDto> Lines);

public record InvoiceLineDto(
    Guid Id,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineTotal,
    Guid? WorkLogId);

public record InvoiceUpsertRequest(
    Guid ProjectId,
    string InvoiceNumber,
    DateTime IssueDate,
    DateTime DueDate,
    InvoiceStatus Status,
    decimal Subtotal,
    decimal TaxAmount,
    decimal TotalAmount,
    decimal AmountPaid,
    IReadOnlyCollection<InvoiceLineUpsertRequest> Lines);

public record InvoiceLineUpsertRequest(
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    Guid? WorkLogId);
