namespace Coossi.Blazor.Data.Models.Prestations;


public sealed record RusPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,

    RusIntroBlock Intro,
    RusDefinitionBlock Definition,
    RusLegalBlock Legal,
    RusCoossiRoleBlock CoossiRole,
    RusMethodologyBlock Methodology,
    RusMissionsBlock Missions,
    RusEstablishmentsBlock Establishments,
    RusAdvantagesBlock Advantages,
    RusContactBlock ContactBlock,

    string CitiesIntro,
    List<string> Cities,

    CtaBlock Cta
);

public sealed record RusIntroBlock(
    string BadgeText,
    string Title,
    List<string> Paragraphs
);

public sealed record RusDefinitionBlock(
    string Title,
    List<string> Paragraphs
);

public sealed record RusLegalBlock(
    string Title,
    string ArticleTitle,
    string ArticleText,
    string HighlightTitle,
    string HighlightText
);

public sealed record RusCoossiRoleBlock(
    string Title,
    string Text
);

public sealed record RusMethodologyBlock(
    string Title,
    List<RusStepItem> Steps
);

public sealed record RusStepItem(
    string Icon,
    string Title,
    string Text
);

public sealed record RusMissionsBlock(
    string Title,
    List<RusMissionCard> Cards
);

public sealed record RusMissionCard(
    string Icon,
    string Title,
    List<string> Bullets
);

public sealed record RusEstablishmentsBlock(
    string Title,
    List<RusEstablishmentItem> Items
);

public sealed record RusEstablishmentItem(
    string Icon,
    string Title,
    string Text
);

public sealed record RusAdvantagesBlock(
    string Title,
    List<RusAdvantageItem> Items
);

public sealed record RusAdvantageItem(
    string Icon,
    string Title,
    string Text
);

public sealed record RusContactBlock(
    string Title,
    string Text,
    string PhoneDisplay,
    string PhoneHref,
    string ButtonText,
    string ButtonHref
);