namespace Coossi.Blazor.Data.Models.Prestations;

public sealed record CoordinationPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,

    // Intro
    List<string> IntroText,

    // Highlights
    MissionBlock MissionHighlight,
    string SuccessAlert,

    // Phases
    string PhasesTitle,
    List<PhaseItem> Phases,

    // Expertise
    string ExpertiseTitle,
    List<ExpertiseItem> Expertise,

    // Bas de page
    string ContactText,
    string CitiesIntro,
    List<string> Cities,
    List<string> Keywords,
    CtaBlock Cta
);

public sealed record MissionBlock(string Icon, string Title, string Text);
public sealed record PhaseItem(string Title, string Icon, List<string> Bullets);
public sealed record ExpertiseItem(string Title, string Icon, string Text);



//public sealed record CoordinationPage(
//    string Slug,
//    SeoBlock Seo,
//    HeroBlock Hero,
//    string IntroText,
//    string ReglementationText,
//    List<CardBlock> Secteurs,   // ERP / Autres secteurs
//    string ContactText,
//    List<string> Cities,
//    CtaBlock Cta
//);

//public sealed record CardBlock(string Title, List<string> Bullets, string? Icon = null);