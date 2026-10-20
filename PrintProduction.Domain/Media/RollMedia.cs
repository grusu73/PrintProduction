namespace PrintProduction.Domain.Media;

public sealed class RollMedia : Media
{
    public required double WidthMm { get; init; }
    public required double LengthM { get; init; }
    public required decimal CostPerMeter { get; init; }

    public decimal CalculateCost(double lengthM)
    {
        if (!double.IsFinite(lengthM) || lengthM <= 0 || lengthM > LengthM)
        {
            throw new ArgumentOutOfRangeException(
                nameof(lengthM),
                "Length must be greater than zero and no longer than the available roll.");
        }

        return CostPerMeter * (decimal)lengthM;
    }

    public override string ToString() =>
        $"{base.ToString()}, {WidthMm:0.##} mm x {LengthM:0.##} m, " +
        $"cost {CostPerMeter:0.##}/m";
}