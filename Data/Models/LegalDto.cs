namespace Coossi.Blazor.Data.Models;


public record LegalSection(
    string Kind,                 // "section" | "box" | "alert" | "contact"
    string? Icon,                // ex: "fa-user-tie"
    string? Heading,             // Titre de section
    string? Html,                // Contenu HTML (p, ul, etc.)
    List<string>? Lines          // Pour les blocs info/listes rapides
);

public record LegalPageDto(
    string Id,
    string Title,
    string Lead,
    List<LegalSection> Sections
);