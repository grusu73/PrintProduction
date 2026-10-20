# Print Production Domain Model Requirements

## 1. Purpose

This document defines a simple initial domain model for a C# project that represents printers, media, print jobs, finishing requirements, and production plans.

The model should preserve the following separation:

- A **PrintJob** describes what must be produced.
- A **Printer** describes the capabilities of an available printing device.
- **Media** describes the physical material available for production.
- A **ProductionPlan** connects a job to the selected printer and media.

## 2. Suggested Project Areas

```text
PrintProduction
|
+-- Printers
|   +-- Printer
|   +-- ContinuousFeedPrinter
|   +-- SheetFedPrinter
|
+-- Media
|   +-- Media
|   +-- RollMedia
|   +-- SheetMedia
|
+-- Jobs
|   +-- PrintJob
|   +-- MediaRequirement
|   +-- FinishingRequirement
|
+-- Production
    +-- ProductionPlan
```

## 3. Printer

### 3.1 Common Printer Properties

| Property | C# Type | Description |
|---|---|---|
| `Id` | `string` | Unique printer identifier |
| `Name` | `string` | Display name |
| `Manufacturer` | `string` | Printer manufacturer |
| `Model` | `string` | Device model |
| `Type` | `PrinterType` | Printer category |
| `Technology` | `PrintTechnology` | Printing technology |
| `SupportsColor` | `bool` | Indicates whether color printing is supported |
| `SupportsDuplex` | `bool` | Indicates whether duplex printing is supported |
| `MinMediaWeightGsm` | `double` | Minimum supported media grammage |
| `MaxMediaWeightGsm` | `double` | Maximum supported media grammage |
| `Status` | `DeviceStatus` | Current device status |

### 3.2 PrinterType Values

- `ContinuousFeed`
- `SheetFed`

### 3.3 PrintTechnology Values

- `Inkjet`
- `Toner`

### 3.4 DeviceStatus Values

- `Available`
- `Busy`
- `Offline`
- `Maintenance`

### 3.5 ContinuousFeedPrinter Properties

In addition to the common printer properties:

| Property | C# Type | Description |
|---|---|---|
| `MinWebWidthMm` | `double` | Minimum supported web width in millimeters |
| `MaxWebWidthMm` | `double` | Maximum supported web width in millimeters |
| `MaxSpeedMetersPerMinute` | `double` | Maximum printing speed in meters per minute |
| `MaxRollDiameterMm` | `double?` | Optional maximum supported roll diameter |

### 3.6 SheetFedPrinter Properties

In addition to the common printer properties:

| Property | C# Type | Description |
|---|---|---|
| `MinSheetWidthMm` | `double` | Minimum supported sheet width |
| `MinSheetHeightMm` | `double` | Minimum supported sheet height |
| `MaxSheetWidthMm` | `double` | Maximum supported sheet width |
| `MaxSheetHeightMm` | `double` | Maximum supported sheet height |
| `MaxSpeedSheetsPerMinute` | `double` | Maximum printing speed in sheets per minute |

## 4. Media

### 4.1 Common Media Properties

| Property | C# Type | Description |
|---|---|---|
| `Id` | `string` | Unique media identifier |
| `Name` | `string` | Media display name |
| `Type` | `MediaType` | Media category |
| `GrammageGsm` | `double` | Media weight in grams per square meter |

### 4.2 MediaType Values

- `Uncoated`
- `Coated`
- `Gloss`
- `Matte`
- `Recycled`
- `Specialty`

### 4.3 RollMedia Properties

In addition to the common media properties:

| Property | C# Type | Description |
|---|---|---|
| `WidthMm` | `double` | Roll width in millimeters |
| `LengthM` | `double` | Roll length in meters |
| `CostPerMeter` | `decimal` | Media cost per linear meter |

### 4.4 SheetMedia Properties

In addition to the common media properties:

| Property | C# Type | Description |
|---|---|---|
| `WidthMm` | `double` | Sheet width in millimeters |
| `HeightMm` | `double` | Sheet height in millimeters |
| `SheetsPerPack` | `int?` | Optional number of sheets per package |
| `CostPerSheet` | `decimal` | Media cost per sheet |

## 5. PrintJob

A print job describes the requested product independently of the printer and concrete media selected for production.

| Property | C# Type | Description |
|---|---|---|
| `Id` | `string` | Unique job identifier |
| `Name` | `string` | Job name |
| `Copies` | `int` | Number of requested copies |
| `PageCount` | `int` | Number of pages per copy |
| `PageWidthMm` | `double` | Finished page width |
| `PageHeightMm` | `double` | Finished page height |
| `ColorMode` | `ColorMode` | Required color mode |
| `PrintSides` | `PrintSides` | Simplex or duplex printing |
| `MediaRequirement` | `MediaRequirement` | Required media characteristics |
| `FinishingRequirements` | `List<FinishingRequirement>` | Requested finishing operations |

### 5.1 ColorMode Values

- `Monochrome`
- `Color`

### 5.2 PrintSides Values

- `Simplex`
- `Duplex`

## 6. MediaRequirement

A media requirement describes the media characteristics requested by a job. It is separate from a concrete media catalog entry so that compatible media can be selected during production planning.

| Property | C# Type | Description |
|---|---|---|
| `MediaType` | `MediaType` | Required media category |
| `GrammageGsm` | `double` | Required media grammage |
| `WidthMm` | `double?` | Optional required media width |
| `HeightMm` | `double?` | Optional required media height |
| `PreferredMediaId` | `string?` | Optional preferred catalog media identifier |

## 7. FinishingRequirement

The first version should keep finishing requirements simple.

| Property | C# Type | Description |
|---|---|---|
| `Operation` | `FinishingOperation` | Requested finishing operation |
| `FoldScheme` | `string?` | Optional fold scheme or code |

### 7.1 FinishingOperation Values

- `None`
- `Cut`
- `Trim`
- `Fold`
- `Crease`
- `Staple`
- `SaddleStitch`
- `PerfectBind`

A later version may replace `FoldScheme` with a dedicated type containing JDF `F_` folding codes and additional fold information.

## 8. ProductionPlan

A production plan represents the concrete production decision that connects a print job with the selected printer and media.

| Property | C# Type | Description |
|---|---|---|
| `Id` | `string` | Unique production plan identifier |
| `Job` | `PrintJob` | Job to be produced |
| `Printer` | `Printer` | Selected printer |
| `Media` | `Media` | Selected media |
| `MediaQuantity` | `double` | Required media quantity |
| `EstimatedPrintTime` | `TimeSpan` | Estimated printing duration |
| `EstimatedMediaCost` | `decimal` | Estimated media cost |
| `EstimatedTotalCost` | `decimal` | Estimated total production cost |
| `Status` | `ProductionStatus` | Current production status |

### 8.1 ProductionStatus Values

- `Planned`
- `Ready`
- `Running`
- `Completed`
- `Failed`

## 9. Model Relationships

```text
Printer
 +-- ContinuousFeedPrinter
 +-- SheetFedPrinter

Media
 +-- RollMedia
 +-- SheetMedia

PrintJob
 +-- MediaRequirement
 +-- FinishingRequirement [0..*]

ProductionPlan
 +-- PrintJob
 +-- Printer
 +-- Media
```

The intended responsibility boundaries are:

```text
Requirement side                  Resource side

PrintJob                          Printer
 +-- MediaRequirement              +-- Device capabilities
 +-- FinishingRequirement

              \                  /
               \                /
                + ProductionPlan
```

## 10. Initial Scope

The initial implementation should focus on four principal concepts:

1. `Printer`
2. `Media`
3. `PrintJob`
4. `ProductionPlan`

The first useful scenario should answer:

> Given a print job, which printer and media can be used to produce it?

