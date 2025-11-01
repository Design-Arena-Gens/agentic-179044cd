namespace RCPS.Core.DTOs;

public record ClientSummaryDto(Guid Id, string Name, string? PrimaryContactName, string? PrimaryContactEmail);

public record ClientDetailDto(
    Guid Id,
    string Name,
    string? PrimaryContactName,
    string? PrimaryContactEmail,
    string? PhoneNumber,
    string? BillingAddress,
    string? ShippingAddress,
    IReadOnlyCollection<ProjectSummaryDto> Projects);

public record ClientUpsertRequest(
    string Name,
    string? PrimaryContactName,
    string? PrimaryContactEmail,
    string? PhoneNumber,
    string? BillingAddress,
    string? ShippingAddress);
