using System.Text.Json.Serialization;
using System.Text.Json;

namespace Coossi.Blazor.Data.Models;

public record PrestationPageDto(
    int Id,
    string Slug,

    // ====== (schéma legacy à plat) ======
    string? MetaTitle,
    string? MetaDescription,
    string? OgImage,

    string? Title,
    string? Subtitle,

    List<PrestationSection>? Sections,

    // ====== (schéma v2 imbriqué) ======
    SeoBlock? Seo,
    PrestationHeroBlock? Hero
)
{
    // Normalisation : si Seo/Hero fournis, on remplit les champs legacy
    public PrestationPageDto Normalize()
    {
        var metaTitle = MetaTitle ?? Seo?.MetaTitle;
        var metaDesc  = MetaDescription ?? Seo?.MetaDescription;
        var ogImg     = OgImage ?? Seo?.OgImage;

        var title     = Title ?? Hero?.Title;
        var subtitle  = Subtitle ?? Hero?.Subtitle;

        return this with { MetaTitle = metaTitle, MetaDescription = metaDesc, OgImage = ogImg, Title = title, Subtitle = subtitle };
    }
}

public record SeoBlock(
    string? MetaTitle,
    string? MetaDescription,
    string? OgImage
);

public record PrestationHeroBlock(
    string? Icon,
    string? Title,
    string? Subtitle
);

// Section flexible (supporte items = strings ou objets)
public record PrestationSection
{
    // "html" | "intro" | "title" | "bullets" | "cards" | "gridCards" | "steps" | "contact" | "cities" | "cta" ...
    [JsonPropertyName("type")] public string Type { get; init; } = "";

    public string? Title { get; init; }
    public string? Lead  { get; init; }
    public string? Html  { get; init; }

    // ⚠️ items polymorphes (strings ou objets)
    public JsonElement? Items { get; init; }

    // colonnes legacy (si tu en as encore)
    public List<List<string>>? Columns { get; init; }

    // Champs spécifiques “contact/cta”
    public string? Phone { get; init; }
    public string? ButtonText { get; init; }
    public string? ButtonHref { get; init; }
}

public record CardItem(
    string? Title,
    string? Text,
    List<string>? Bullets,
    string? Icon,
    string? Badge
);

public record StepItem(
    string? Title,
    string? Text
);
