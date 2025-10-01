#nullable enable
namespace Coossi.Blazor.Data.Models;

public record SeoSiteDefaults(
    string SiteName,
    string BaseUrl,
    string DefaultTitleSuffix,
    string DefaultDescription,
    string DefaultOgImage,
    string Locale,
    string TwitterCard
);

public record SeoMeta(
    string? Title,
    string? Description,
    string? Canonical,
    string? OgImage,
    bool? Noindex
);
