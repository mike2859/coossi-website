namespace Coossi.Blazor.Data.Models.Prestations;


public sealed record CoordinationPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,
    string IntroText,
    string ReglementationText,
    List<CardBlock> Secteurs,   // ERP / Autres secteurs
    string ContactText,
    List<string> Cities,
    CtaBlock Cta
);

//public sealed record CardBlock(string Title, List<string> Bullets, string? Icon = null);