using PrintProduction.Domain.Jobs;
using PrintProduction.Domain.Media;
using PrintProduction.Domain.Printers;

namespace PrintProduction.Domain.Production;

public enum ProductionStatus
{
    Planned,
    Ready,
    Running,
    Completed,
    Failed
}

public sealed class ProductionPlan
{
    public required string Id { get; init; }
    public required PrintJob Job { get; init; }
    public required Printer Printer { get; init; }
    public required PrintProduction.Domain.Media.Media Media { get; init; }
    public required double MediaQuantity { get; init; }
    public required TimeSpan EstimatedPrintTime { get; init; }
    public required decimal EstimatedMediaCost { get; init; }
    public required decimal EstimatedTotalCost { get; init; }
    public required ProductionStatus Status { get; init; }
}