using System.Text.Json;
using System.Text.Json.Serialization;

namespace PrintProduction.Domain.Media;

public sealed class MediaCatalog
{
    private static readonly JsonSerializerOptions SerializerOptions = CreateSerializerOptions();
    private readonly List<Media> _mediaItems = [];

    public string Serialize() => JsonSerializer.Serialize(_mediaItems, SerializerOptions);

    public static MediaCatalog Deserialize(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        var mediaItems = JsonSerializer.Deserialize<List<Media>>(json, SerializerOptions)
            ?? throw new JsonException("Media catalog JSON must contain an array.");

        var catalog = new MediaCatalog();
        foreach (var media in mediaItems)
        {
            if (media is null)
            {
                throw new JsonException("Media catalog JSON cannot contain null entries.");
            }

            if (!catalog.Add(media))
            {
                throw new JsonException($"Media catalog JSON contains duplicate ID '{media.Id}'.");
            }
        }

        return catalog;
    }

    public bool Add(Media media)
    {
        ArgumentNullException.ThrowIfNull(media);

        if (_mediaItems.Any(existing => string.Equals(existing.Id, media.Id, StringComparison.Ordinal)))
        {
            return false;
        }

        _mediaItems.Add(media);
        return true;
    }

    public IReadOnlyList<Media> GetAll() => _mediaItems.AsReadOnly();

    public Media? GetById(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return _mediaItems.FirstOrDefault(media =>
            string.Equals(media.Id, id, StringComparison.Ordinal));
    }

    public bool Update(Media media)
    {
        ArgumentNullException.ThrowIfNull(media);

        var index = _mediaItems.FindIndex(existing =>
            string.Equals(existing.Id, media.Id, StringComparison.Ordinal));
        if (index < 0)
        {
            return false;
        }

        _mediaItems[index] = media;
        return true;
    }

    public bool Remove(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var index = _mediaItems.FindIndex(media =>
            string.Equals(media.Id, id, StringComparison.Ordinal));
        if (index < 0)
        {
            return false;
        }

        _mediaItems.RemoveAt(index);
        return true;
    }

    public IReadOnlyList<Media> GetByType(MediaType type) => _mediaItems
        .Where(media => media.Type == type)
        .ToList()
        .AsReadOnly();

    public IReadOnlyList<SheetMedia> GetAllSheets() => _mediaItems
        .OfType<SheetMedia>()
        .ToList()
        .AsReadOnly();

    public IReadOnlyList<RollMedia> GetAllRolls() => _mediaItems
        .OfType<RollMedia>()
        .ToList()
        .AsReadOnly();

    public IReadOnlyList<SheetMedia> GetSheetsByDimensions(double widthMm, double heightMm) => _mediaItems
        .OfType<SheetMedia>()
        .Where(media => media.WidthMm == widthMm && media.HeightMm == heightMm)
        .ToList()
        .AsReadOnly();

    public IReadOnlyList<RollMedia> GetRollsByWidth(double widthMm) => _mediaItems
        .OfType<RollMedia>()
        .Where(media => media.WidthMm == widthMm)
        .ToList()
        .AsReadOnly();

    public IReadOnlyList<Media> GetByGrammageRange(double minGsm, double maxGsm)
    {
        if (minGsm > maxGsm)
        {
            throw new ArgumentException("Minimum grammage cannot exceed maximum grammage.", nameof(minGsm));
        }

        return _mediaItems
            .Where(media => media.GrammageGsm >= minGsm && media.GrammageGsm <= maxGsm)
            .ToList()
            .AsReadOnly();
    }

    private static JsonSerializerOptions CreateSerializerOptions()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }
}