namespace PrintProduction.Domain.Printers;

public sealed class SheetFedPrinter : Printer
{
    public override PrinterType Type => PrinterType.SheetFed;
    public required double MinSheetWidthMm { get; init; }
    public required double MinSheetHeightMm { get; init; }
    public required double MaxSheetWidthMm { get; init; }
    public required double MaxSheetHeightMm { get; init; }
    public required double MaxSpeedSheetsPerMinute { get; init; }

    public override string ToString() =>
        $"{base.ToString()}, sheet size {MinSheetWidthMm:0.##} x {MinSheetHeightMm:0.##} mm " +
        $"to {MaxSheetWidthMm:0.##} x {MaxSheetHeightMm:0.##} mm, " +
        $"up to {MaxSpeedSheetsPerMinute:0.##} sheets/min";
}