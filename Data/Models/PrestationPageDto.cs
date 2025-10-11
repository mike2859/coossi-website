#nullable enable
namespace Coossi.Blazor.Data.Models;


public record PrestationPageDto(
    int Id,
    string Slug,

    // SEO intégrés à la fiche
    string MetaTitle,
    string? MetaDescription,
    string? OgImage,

    // Header
    string Title,
    string? Subtitle,

    // Sections de contenu
    List<PrestationSection>? Sections
);

public record PrestationSection(
    string Kind,                // "html" | "bullets" | "columns2" | "faq" | "cta"
    string? Title,
    string? Lead,
    string? Html,               // pour "html"
    List<string>? Items,        // pour "bullets" / "faq" (faq = "Question::Réponse")
    List<List<string>>? Columns // pour "columns2" (deux colonnes de listes)
);


