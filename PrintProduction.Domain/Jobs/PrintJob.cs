using PrintProduction.Domain.Media;

namespace PrintProduction.Domain.Jobs;

public enum ColorMode
{
    Monochrome,
    Color
}

public enum PrintSides
{
    Simplex,
    Duplex
}

public sealed class PrintJob
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required int Copies { get; init; }
    public required int PageCount { get; init; }
    public required double PageWidthMm { get; init; }
    public required double PageHeightMm { get; init; }
    public required ColorMode ColorMode { get; init; }
    public required PrintSides PrintSides { get; init; }
    public required MediaRequirement MediaRequirement { get; init; }
    public List<FinishingRequirement> FinishingRequirements { get; init; } = [];

    public override string ToString()
    {
        var finishing = FinishingRequirements.Count == 0
            ? "None"
            : string.Join(", ", FinishingRequirements.Select(requirement => requirement.Operation));

        return $"{Name} [{Id}] - {Copies:N0} copies, {PageCount} pages, " +
            $"{PageWidthMm:0.##} x {PageHeightMm:0.##} mm, {ColorMode}, {PrintSides}; " +
            $"media {MediaRequirement.MediaType} {MediaRequirement.GrammageGsm:0.##} gsm; " +
            $"finishing: {finishing}";
    }
}