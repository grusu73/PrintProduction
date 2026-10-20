using PrintProduction.Domain.Media;

namespace PrintProduction.Domain.Jobs;

public sealed class MediaRequirement
{
    public required MediaType MediaType { get; init; }
    public required double GrammageGsm { get; init; }
    public double? WidthMm { get; init; }
    public double? HeightMm { get; init; }
    public string? PreferredMediaId { get; init; }
}