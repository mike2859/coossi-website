namespace Coossi.Blazor.Data.Models;

public record RealisationsPage(string Id, string Title, string Lead, List<RealSection> Sections);

public record RealSection(
    string Kind,                    // "stats" | "testimonials" | "missions" | "zone" | "cta"
    string? Title,
    string? Lead,
    List<StatItem>? Items,
    List<TestimonialCard>? Cards,
    List<MissionCard>? Cards2,      // (facultatif si tu préfères séparer)
    string? AreasTitle,
    List<string>? Areas,
    string? CitiesTitle,
    List<string>? Cities,
    RealBox? Box,
    string? Text,
    string? Phone,
    string? PhoneHref,
    string? ContactHref
);

public record TestimonialCard(int Rating, string Content, string ClientName, string ClientCompany);

public record MissionCard(string Type, string Title, List<string> Bullets, string? Note, string? NoteIcon, string? NoteClass);

public record RealBox(string Icon, string Title, string Text);
