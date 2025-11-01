namespace RCPS.Core.DTOs;

public record ProfitabilityTrendPoint(DateTime Period, decimal Revenue, decimal Cost, decimal Margin);

public record ResourceUtilizationPoint(string ResourceName, decimal Allocated, decimal Utilized);

public record AgingBucket(string Label, decimal Amount);

public record DashboardSnapshotDto(
    decimal TotalRecognizedRevenue,
    decimal TotalCost,
    decimal TotalBilled,
    decimal GrossMarginPercentage,
    IReadOnlyCollection<ProfitabilityTrendPoint> ProfitabilityTrend,
    IReadOnlyCollection<ResourceUtilizationPoint> ResourceUtilization,
    IReadOnlyCollection<AgingBucket> InvoiceAging);
