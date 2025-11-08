namespace Coossi.Blazor.Data.Models.Prestations;



public sealed record AssistanceMoePage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,

    AmoMissionBlock Mission,
    AmoMissionsBlock MissionsCoordinateur,
    AmoRealisationBlock Realisation,
    AmoBureauBlock BureauEtudes,
    AmoExpertisesBlock Expertises,
    AmoPlansBlock PlansDocs,
    AmoDoeBlock Doe,
    AmoContactBlock Contact,

    string CitiesIntro,
    List<string> Cities,

    CtaBlock Cta
);

public sealed record AmoMissionBlock(string Title, List<string> Paragraphs);

public sealed record AmoMissionsBlock(
    string Title,
    string Intro,
    List<AmoCard> Cards
);

public sealed record AmoCard(
    string Icon,
    string Title,
    string Text
);

public sealed record AmoRealisationBlock(
    string Title,
    string Badge,
    string Intro,
    string SubTitle,
    string SubText,
    List<string> Bullets
);

public sealed record AmoBureauBlock(
    string Title,
    string Badge,
    string Text
);

public sealed record AmoExpertisesBlock(
    string Title,
    string Intro,
    List<AmoCard> Items
);

public sealed record AmoPlansBlock(
    string Title,
    string Intro,
    List<string> Items,
    string ClosingText
);

public sealed record AmoDoeBlock(
    string Title,
    string Intro,
    string HighlightTitle,
    string HighlightLead,
    string HighlightText,
    string Outro,
    string Note
);

public sealed record AmoContactBlock(
    string Title,
    string Text,
    string PhoneDisplay,
    string PhoneHref,
    string ButtonText,
    string ButtonHref
);