using System.Text.Json.Serialization;

namespace PrintProduction.Domain.Printers;

public enum PrinterType
{
    ContinuousFeed,
    SheetFed
}

public enum PrintTechnology
{
    Inkjet,
    Toner
}

public enum DeviceStatus
{
    Available,
    Busy,
    Offline,
    Maintenance
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(ContinuousFeedPrinter), "continuous-feed")]
[JsonDerivedType(typeof(SheetFedPrinter), "sheet-fed")]
public abstract class Printer
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Manufacturer { get; init; }
    public required string Model { get; init; }
    public abstract PrinterType Type { get; }
    public required PrintTechnology Technology { get; init; }
    public required bool SupportsColor { get; init; }
    public required bool SupportsDuplex { get; init; }
    public required double MinMediaWeightGsm { get; init; }
    public required double MaxMediaWeightGsm { get; init; }
    public required DeviceStatus Status { get; init; }

    public string GetDescription() => $"{Manufacturer} {Model} - {Name}";

    public bool CanPrintColor() => SupportsColor;

    public override string ToString() =>
        $"{Name} [{Id}] - {Manufacturer} {Model} ({Type}, {Technology}, {Status}; " +
        $"color {(SupportsColor ? "supported" : "not supported")}, " +
        $"duplex {(SupportsDuplex ? "supported" : "not supported")}; " +
        $"{MinMediaWeightGsm:0.##}-{MaxMediaWeightGsm:0.##} gsm)";
}