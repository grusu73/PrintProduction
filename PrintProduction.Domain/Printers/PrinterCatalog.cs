using System.Text.Json;
using System.Text.Json.Serialization;

namespace PrintProduction.Domain.Printers;

public sealed class PrinterCatalog
{
    private static readonly JsonSerializerOptions SerializerOptions = CreateSerializerOptions();
    private readonly List<Printer> _printers = [];

    public string Serialize() => JsonSerializer.Serialize(_printers, SerializerOptions);

    public static PrinterCatalog Deserialize(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        var printers = JsonSerializer.Deserialize<List<Printer>>(json, SerializerOptions)
            ?? throw new JsonException("Printer catalog JSON must contain an array.");

        var catalog = new PrinterCatalog();
        foreach (var printer in printers)
        {
            if (printer is null)
            {
                throw new JsonException("Printer catalog JSON cannot contain null entries.");
            }

            if (!catalog.Add(printer))
            {
                throw new JsonException($"Printer catalog JSON contains duplicate ID '{printer.Id}'.");
            }
        }

        return catalog;
    }

    public bool Add(Printer printer)
    {
        ArgumentNullException.ThrowIfNull(printer);

        if (_printers.Any(existing => string.Equals(existing.Id, printer.Id, StringComparison.Ordinal)))
        {
            return false;
        }

        _printers.Add(printer);
        return true;
    }

    public IReadOnlyList<Printer> GetAll() => _printers.AsReadOnly();

    public Printer? GetById(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return _printers.FirstOrDefault(printer =>
            string.Equals(printer.Id, id, StringComparison.Ordinal));
    }

    public bool Update(Printer printer)
    {
        ArgumentNullException.ThrowIfNull(printer);

        var index = _printers.FindIndex(existing =>
            string.Equals(existing.Id, printer.Id, StringComparison.Ordinal));
        if (index < 0)
        {
            return false;
        }

        _printers[index] = printer;
        return true;
    }

    public bool Remove(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var index = _printers.FindIndex(printer =>
            string.Equals(printer.Id, id, StringComparison.Ordinal));
        if (index < 0)
        {
            return false;
        }

        _printers.RemoveAt(index);
        return true;
    }

    public IReadOnlyList<Printer> Search(string searchTerm)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(searchTerm);

        return _printers
            .Where(printer =>
                printer.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                printer.Model.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<Printer> GetByManufacturer(string manufacturer)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(manufacturer);

        return _printers
            .Where(printer => printer.Manufacturer.Contains(manufacturer, StringComparison.OrdinalIgnoreCase))
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<Printer> GetByType(PrinterType type) => _printers
        .Where(printer => printer.Type == type)
        .ToList()
        .AsReadOnly();

    private static JsonSerializerOptions CreateSerializerOptions()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }
}