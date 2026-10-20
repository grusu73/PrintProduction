namespace PrintProduction.Domain.Printers;

public sealed class ContinuousFeedPrinter : Printer
{
    public override PrinterType Type => PrinterType.ContinuousFeed;
    public required double MinWebWidthMm { get; init; }
    public required double MaxWebWidthMm { get; init; }
    public double? MaxRollDiameterMm { get; init; }
    public required double MaxSpeedMetersPerMinute { get; init; }

    public bool SupportsWebWidth(double widthMm) =>
        double.IsFinite(widthMm) && widthMm >= MinWebWidthMm && widthMm <= MaxWebWidthMm;

    public override string ToString()
    {
        var maxRollDiameter = MaxRollDiameterMm.HasValue
            ? $", max roll diameter {MaxRollDiameterMm.Value:0.##} mm"
            : string.Empty;

        return $"{base.ToString()}, web width {MinWebWidthMm:0.##}-{MaxWebWidthMm:0.##} mm, " +
            $"up to {MaxSpeedMetersPerMinute:0.##} m/min{maxRollDiameter}";
    }
}