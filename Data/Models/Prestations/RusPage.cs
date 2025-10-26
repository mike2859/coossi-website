namespace Coossi.Blazor.Data.Models.Prestations;


public sealed record RusPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,
    string SinceText,
    string QuEstCeQueText,
    string CadreLegalText,
    string EngagementText,
    string CoossiRoleText,
    List<CardBlock> Methodologie,   // 3 étapes
    List<CardBlock> Missions,       // 4 cartes à puces
    List<CardBlock> Etablissements, // 3 cartes
    List<CardBlock> Avantages,      // 4 cartes
    string ContactText,
    List<string> Cities,
    CtaBlock Cta
);
