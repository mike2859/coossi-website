namespace Coossi.Blazor.Data.Models.Prestations;

public sealed record DuerpPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,

    // Contenu simple et clair, adapté au JSON de DUERP
    string IntroText,
    string ObjectifTitle,
    List<string> ContenuItems,
    List<string> Risques,
    List<string> MiseAJour,
    List<string> Formats,
    string ContactHtml,
    List<string> Cities,
    CtaBlock Cta
);