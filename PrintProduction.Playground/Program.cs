using PrintProduction.Domain.Jobs;
using PrintProduction.Domain.Media;
using PrintProduction.Domain.Printers;
using PrintProduction.Domain.Production;
using MediaStock = PrintProduction.Domain.Media.Media;

var printerCatalog = new PrinterCatalog();
var rollPrinter = new ContinuousFeedPrinter
{
	Id = "PRN-ROLL-01",
	Name = "RollJet 9000",
	Manufacturer = "Acme Print",
	Model = "RJ-9000",
	Technology = PrintTechnology.Inkjet,
	SupportsColor = true,
	SupportsDuplex = true,
	MinMediaWeightGsm = 60,
	MaxMediaWeightGsm = 350,
	Status = DeviceStatus.Available,
	MinWebWidthMm = 330,
	MaxWebWidthMm = 1620,
	MaxRollDiameterMm = 1000,
	MaxSpeedMetersPerMinute = 250
};
var sheetPrinter = new SheetFedPrinter
{
	Id = "PRN-SHEET-01",
	Name = "PressMaster A3",
	Manufacturer = "Northstar Press",
	Model = "PM-A3",
	Technology = PrintTechnology.Toner,
	SupportsColor = true,
	SupportsDuplex = true,
	MinMediaWeightGsm = 70,
	MaxMediaWeightGsm = 400,
	Status = DeviceStatus.Available,
	MinSheetWidthMm = 148,
	MinSheetHeightMm = 210,
	MaxSheetWidthMm = 330,
	MaxSheetHeightMm = 488,
	MaxSpeedSheetsPerMinute = 150
};
printerCatalog.Add(rollPrinter);
printerCatalog.Add(sheetPrinter);

var restoredPrinterCatalog = PrinterCatalog.Deserialize(printerCatalog.Serialize());
Console.WriteLine(
	$"Printer JSON round-trip restored {restoredPrinterCatalog.GetAll().Count} printers, " +
	$"including {restoredPrinterCatalog.GetByType(PrinterType.SheetFed).Count} sheet-fed and " +
	$"{restoredPrinterCatalog.GetByType(PrinterType.ContinuousFeed).Count} continuous-feed.");

Console.WriteLine("=== Printer catalog ===");
PrintItems("All printers", printerCatalog.GetAll());
Console.WriteLine($"Duplicate printer ID rejected: {!printerCatalog.Add(sheetPrinter)}");
Console.WriteLine($"Lookup PRN-SHEET-01: {printerCatalog.GetById("PRN-SHEET-01")?.GetDescription()}");
Console.WriteLine($"Search name/model 'press': {printerCatalog.Search("press").Count} match(es)");
Console.WriteLine($"Search manufacturer 'acme': {printerCatalog.GetByManufacturer("acme").Count} match(es)");
Console.WriteLine($"Search sheet-fed printers: {printerCatalog.GetByType(PrinterType.SheetFed).Count} match(es)");
Console.WriteLine($"PressMaster can print color: {sheetPrinter.CanPrintColor()}");
Console.WriteLine($"Roll printer supports 400 mm web: {rollPrinter.SupportsWebWidth(400)}");
Console.WriteLine($"Roll printer supports 180 mm web: {rollPrinter.SupportsWebWidth(180)}");
Console.WriteLine($"Roll printer supports 1700 mm web: {rollPrinter.SupportsWebWidth(1700)}");

var updatedSheetPrinter = new SheetFedPrinter
{
	Id = sheetPrinter.Id,
	Name = sheetPrinter.Name,
	Manufacturer = sheetPrinter.Manufacturer,
	Model = sheetPrinter.Model,
	Technology = sheetPrinter.Technology,
	SupportsColor = sheetPrinter.SupportsColor,
	SupportsDuplex = sheetPrinter.SupportsDuplex,
	MinMediaWeightGsm = sheetPrinter.MinMediaWeightGsm,
	MaxMediaWeightGsm = sheetPrinter.MaxMediaWeightGsm,
	Status = sheetPrinter.Status,
	MinSheetWidthMm = sheetPrinter.MinSheetWidthMm,
	MinSheetHeightMm = sheetPrinter.MinSheetHeightMm,
	MaxSheetWidthMm = sheetPrinter.MaxSheetWidthMm,
	MaxSheetHeightMm = sheetPrinter.MaxSheetHeightMm,
	MaxSpeedSheetsPerMinute = 145
};
Console.WriteLine($"Update printer: {printerCatalog.Update(updatedSheetPrinter)}");
Console.WriteLine($"Updated printer: {printerCatalog.GetById(sheetPrinter.Id)}");
Console.WriteLine($"Remove printer: {printerCatalog.Remove(sheetPrinter.Id)}");
Console.WriteLine($"Remove missing printer: {printerCatalog.Remove("PRN-MISSING")}");
Console.WriteLine($"Add updated printer back: {printerCatalog.Add(updatedSheetPrinter)}");

var mediaCatalog = new MediaCatalog();
var coatedRoll = new RollMedia
{
	Id = "MED-ROLL-01",
	Name = "Coated roll",
	Type = MediaType.Coated,
	GrammageGsm = 120,
	WidthMm = 720,
	LengthM = 500,
	CostPerMeter = 0.45m
};
var standardRoll = new RollMedia
{
	Id = "MED-ROLL-02",
	Name = "Standard roll",
	Type = MediaType.Uncoated,
	GrammageGsm = 90,
	WidthMm = 520,
	LengthM = 2000,
	CostPerMeter = 0.15m
};
var coatedA4 = new SheetMedia
{
	Id = "MED-SHEET-01",
	Name = "Coated A4",
	Type = MediaType.Coated,
	GrammageGsm = 120,
	WidthMm = 210,
	HeightMm = 297,
	SheetsPerPack = 500,
	CostPerSheet = 0.08m
};
var coatedA3 = new SheetMedia
{
	Id = "MED-SHEET-02",
	Name = "Coated A3",
	Type = MediaType.Coated,
	GrammageGsm = 170,
	WidthMm = 297,
	HeightMm = 420,
	SheetsPerPack = 250,
	CostPerSheet = 0.16m
};
mediaCatalog.Add(coatedRoll);
mediaCatalog.Add(standardRoll);
mediaCatalog.Add(coatedA4);
mediaCatalog.Add(coatedA3);

var restoredMediaCatalog = MediaCatalog.Deserialize(mediaCatalog.Serialize());
Console.WriteLine(
	$"Media JSON round-trip restored {restoredMediaCatalog.GetAll().Count} media entries, " +
	$"including {restoredMediaCatalog.GetAllSheets().Count} sheets and " +
	$"{restoredMediaCatalog.GetAllRolls().Count} rolls.");

Console.WriteLine();
Console.WriteLine("=== Media catalog ===");
PrintItems("All media", mediaCatalog.GetAll());
Console.WriteLine($"Duplicate media ID rejected: {!mediaCatalog.Add(coatedA4)}");
Console.WriteLine($"Lookup MED-SHEET-01: {mediaCatalog.GetById("MED-SHEET-01")?.GetDescription()}");
PrintItems("Sheet media", mediaCatalog.GetAllSheets());
PrintItems("Roll media", mediaCatalog.GetAllRolls());
PrintItems("Exact 210 x 297 mm sheets", mediaCatalog.GetSheetsByDimensions(210, 297));
PrintItems("Rolls exactly 520 mm wide", mediaCatalog.GetRollsByWidth(520));
PrintItems("Media from 100 to 130 gsm", mediaCatalog.GetByGrammageRange(100, 130));
PrintItems("Media from 200 to 250 gsm (expected empty)", mediaCatalog.GetByGrammageRange(200, 250));

var updatedA4 = new SheetMedia
{
	Id = coatedA4.Id,
	Name = coatedA4.Name,
	Type = coatedA4.Type,
	GrammageGsm = coatedA4.GrammageGsm,
	WidthMm = coatedA4.WidthMm,
	HeightMm = coatedA4.HeightMm,
	SheetsPerPack = coatedA4.SheetsPerPack,
	CostPerSheet = 0.09m
};
Console.WriteLine($"Update media: {mediaCatalog.Update(updatedA4)}");
Console.WriteLine($"Updated media: {mediaCatalog.GetById(coatedA4.Id)}");
Console.WriteLine($"Remove media: {mediaCatalog.Remove(standardRoll.Id)}");
Console.WriteLine($"Remove missing media: {mediaCatalog.Remove("MED-MISSING")}");
Console.WriteLine($"Add removed roll back: {mediaCatalog.Add(standardRoll)}");
Console.WriteLine($"A4 area: {updatedA4.GetAreaSquareMeters():0.00000} m^2");
Console.WriteLine($"Cost for 25 m of coated roll: {coatedRoll.CalculateCost(25):0.00}");

var jobs = new List<PrintJob>
{
	new PrintJob
	{
		Id = "JOB-001",
		Name = "Product Brochure",
		Copies = 5000,
		PageCount = 24,
		PageWidthMm = 210,
		PageHeightMm = 297,
		ColorMode = ColorMode.Color,
		PrintSides = PrintSides.Duplex,
		MediaRequirement = new MediaRequirement
		{
			MediaType = MediaType.Coated,
			GrammageGsm = 120,
			WidthMm = 210,
			HeightMm = 297,
			PreferredMediaId = "MED-SHEET-01"
		},
		FinishingRequirements =
		[
			new FinishingRequirement { Operation = FinishingOperation.SaddleStitch }
		]
	}
};

var plan = new ProductionPlan
{
	Id = "PLAN-001",
	Job = jobs[0],
	Printer = updatedSheetPrinter,
	Media = updatedA4,
	MediaQuantity = 60_000,
	EstimatedPrintTime = TimeSpan.FromHours(7),
	EstimatedMediaCost = 4800m,
	EstimatedTotalCost = 6500m,
	Status = ProductionStatus.Planned
};

Console.WriteLine();
Console.WriteLine("=== Existing print-job and plan examples ===");
PrintItems("Print jobs", jobs);
PrintPlan(plan);

static void PrintItems<T>(string title, IEnumerable<T> items)
{
	Console.WriteLine($"\n{title}:");
	var foundItems = false;
	foreach (var item in items)
	{
		foundItems = true;
		Console.WriteLine($"  {item}");
	}

	if (!foundItems)
	{
		Console.WriteLine("  (none)");
	}
}

static void PrintPlan(ProductionPlan plan)
{
	Console.WriteLine($"\nProduction plan [{plan.Id}] - {plan.Status}");
	Console.WriteLine($"  Job: {plan.Job}");
	Console.WriteLine($"  Printer: {plan.Printer}");
	Console.WriteLine($"  Media: {plan.Media}");
	Console.WriteLine($"  Media quantity: {plan.MediaQuantity:N0}");
	Console.WriteLine($"  Estimated print time: {plan.EstimatedPrintTime}");
	Console.WriteLine($"  Estimated media cost: {plan.EstimatedMediaCost:N2}");
	Console.WriteLine($"  Estimated total cost: {plan.EstimatedTotalCost:N2}");
}
