namespace Coossi.Blazor.Data.Models.Prestations;

public sealed record AuditDiagnosticPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,
    string IntroPourquoi,
    string IntroPreservation,
    List<string> Etapes,
    string IntroIntervention,
    List<CardBlock> Etablissements,
    List<CardBlock> PointsDeControle,
    string LivrableText,
    string ContactText,
    List<string> Cities,
    CtaBlock Cta
);

public sealed record CardBlock(string Title, List<string>? Bullets = null, string? Text = null, string? Icon = null, string? Badge = null);