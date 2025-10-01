namespace Coossi.Blazor.Data.Models;

public record ServicesPage(string Id, string Title, string Lead, List<ServiceSection> Sections);

public record ServiceSection(
    string Kind,                 // "service" | "types" | "cta" ...
    string? Id,
    string? Icon,
    string? Title,
    string? LeadHtml,
    string? FeaturesTitle,
    List<string>? Features,
    List<string>? ZoneTags,
    ServiceBox? RightBox,
    List<ServiceBox>? LeftBoxes,
    bool? Reverse,
    string? Bg,
    string? Subtitle,
    List<TypeCard>? Cards,
    string? Text,
    string? Phone,
    string? PhoneHref,
    string? ContactHref
);

public record ServiceBox(string Icon, string Title, string Text, List<string>? Bullets);
public record TypeCard(string Icon, string Title, string Text, string? Small);
