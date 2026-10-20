namespace PrintProduction.Domain.Jobs;

public enum FinishingOperation
{
    None,
    Cut,
    Trim,
    Fold,
    Crease,
    Staple,
    SaddleStitch,
    PerfectBind
}

public sealed class FinishingRequirement
{
    public required FinishingOperation Operation { get; init; }
    public string? FoldScheme { get; init; }
}