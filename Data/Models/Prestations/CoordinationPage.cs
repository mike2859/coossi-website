namespace Coossi.Blazor.Data.Models.Prestations;

public sealed record CoordinationPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,

    IntroBlockPrestation Intro,
    LegalBlock Legal,
    MissionsBlock Missions,
    MethodoBlock Methodo,
    EstablishmentsBlock Establishments,
    AdvantagesBlock Advantages,
    ContactBlock Contact,

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


public sealed record IntroBlockPrestation(string Title, List<string> Paragraphs);

public sealed record LegalBlock(
    string Title,
    List<string> Paragraphs,
    string HighlightTitle,
    string HighlightText
);

public sealed record MissionsBlock(
    string Title,
    string Lead,
    List<MissionItem> Items
);
public sealed record MissionItem(string Title, string Icon, List<string> Bullets);

public sealed record MethodoBlock(
    string Title,
    string Lead,
    List<MethodStep> Steps
);
public sealed record MethodStep(string Title, string Icon, List<string> Bullets);

//public sealed record EstablishmentsBlock(
//    string Title,
//    List<EstablishmentItem> Items
//);
//public sealed record EstablishmentItem(string Title, string Icon, string Subtitle, string Small);

public sealed record AdvantagesBlock(
    string Title,
    List<string> LeftBullets,
    List<string> RightBullets
);

public sealed record ContactBlock(
    string Title,
    string Text,
    string PhoneDisplay,
    string PhoneHref,
    string ButtonText,
    string ButtonHref
);


// public sealed record MissionBlock(string Icon, string Title, string Text);
// public sealed record PhaseItem(string Title, string Icon, List<string> Bullets);
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