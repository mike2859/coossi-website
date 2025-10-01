namespace Coossi.Blazor.Data.Models;



public record TrustPage(string Id, string Title, string Lead, List<TrustSection> Sections);

public record TrustSection(
    string Kind,                  // "intro" | "stats" | "sector" | "partners" | "cta"
    string? Title,
    string? Lead,
    string? Html,
    List<StatItem>? Items,
    List<TrustCard>? Cards,
    List<PartnerBadge>? Badges,
    string? Icon,
    string? Text,
    string? Phone,
    string? PhoneHref,
    string? ContactHref,
    string? Subtitle
);



public record TrustCard(string Icon, string Name, string Sector, string Highlight);

public record PartnerBadge(string Icon, string Title, string Text);