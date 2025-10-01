namespace Coossi.Blazor.Data.Models;
public record PrestationDto(
    int Id,
    string Slug,
    string Title,
    string Subtitle,
    string? MetaTitle,
    string? MetaDescription,
    string IntroHtml,
    string BodyHtml,
    string? HeroImageWebp,
    string? HeroImagePngFallback,
    string HeroTitle,
    string HeroLead,
    string? SeoImageWebp,
    string? SeoImagePngFallback,
    IEnumerable<string>? Bullets = null
);