# PrintProduction

A small .NET domain model and console demonstration for managing print-production resources.

## Projects

- `PrintProduction.Domain` contains printer and media models, catalogs, and the current print-job and production-plan data models.
- `PrintProduction.Playground` demonstrates domain and catalog behavior without placing console interaction in the Domain library.

## Build and run

From the repository root:

```powershell
dotnet build PrintProduction.sln
dotnet run --project PrintProduction.Playground/PrintProduction.Playground.csproj
```

## Catalogs

`PrinterCatalog` supports adding, listing, retrieving by ID, updating, removing, and searching by name/model, manufacturer, and printer type. IDs are immutable after initialization and duplicate IDs are rejected.

`MediaCatalog` supports adding, listing, retrieving by ID, updating, and removing media. It can retrieve all sheet or roll media, filter material categories, find sheets by exact width and height, find rolls by exact width, and filter by an inclusive grammage range. Dimension filters use exact millimeter matches, not tolerances.

Both catalogs provide `Serialize()` and `Deserialize(string json)` methods for JSON round-tripping. The JSON preserves printer and media subtypes and writes enum values as strings. File storage remains the responsibility of the application layer.

## Calculations and validation

`SheetMedia.GetAreaSquareMeters()` returns the area of one sheet. `RollMedia.CalculateCost(lengthM)` calculates media cost and rejects non-finite, non-positive lengths or lengths greater than the available roll. `ContinuousFeedPrinter.SupportsWebWidth(widthMm)` checks an inclusive supported-width range.

The Playground demonstrates successful and unsuccessful catalog operations, searches, filters, and these calculations. The production-plan example is sample data; automatic compatibility checking and estimation are later project milestones.
