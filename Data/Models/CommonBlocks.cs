namespace Coossi.Blazor.Data.Models;

public sealed record SeoBlock(string MetaTitle, string MetaDescription, string? OgImage);
public sealed record HeroBlock(string Title, string? Subtitle, string? Icon = null);

public sealed record CtaBlock(string Title, string? Text, string ButtonText, string ButtonHref);