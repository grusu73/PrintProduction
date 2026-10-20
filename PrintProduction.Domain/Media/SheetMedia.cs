namespace PrintProduction.Domain.Media;

public sealed class SheetMedia : Media
{
    public required double WidthMm { get; init; }
    public required double HeightMm { get; init; }
    public int? SheetsPerPack { get; init; }
    public required decimal CostPerSheet { get; init; }

    public double GetAreaSquareMeters() => WidthMm * HeightMm / 1_000_000;

    public override string ToString()
    {
        var packSize = SheetsPerPack.HasValue
            ? $"{SheetsPerPack.Value} sheets/pack"
            : "pack size unspecified";

        return $"{base.ToString()}, {WidthMm:0.##} x {HeightMm:0.##} mm, " +
            $"{packSize}, cost {CostPerSheet:0.##}/sheet";
    }
}