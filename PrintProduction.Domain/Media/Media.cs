using System.Text.Json.Serialization;

namespace PrintProduction.Domain.Media;

public enum MediaType
{
    Uncoated,
    Coated,
    Gloss,
    Matte,
    Recycled,
    Specialty
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(SheetMedia), "sheet")]
[JsonDerivedType(typeof(RollMedia), "roll")]
public abstract class Media
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required MediaType Type { get; init; }
    public required double GrammageGsm { get; init; }

    public string GetDescription() => $"{Name}, {GrammageGsm:0.##} gsm";

    public override string ToString() =>
        $"{Name} [{Id}] - {Type}, {GrammageGsm:0.##} gsm";
}