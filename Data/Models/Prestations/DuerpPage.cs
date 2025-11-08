namespace Coossi.Blazor.Data.Models.Prestations;


public sealed record DuerpPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,

    DuerpIntroBlock Intro,
    ObjectifBlock Objectif,
    ContenuBlock Contenu,
    RiskTypesBlock RiskTypes,
    MiseAJourBlock MiseAJour,
    FormatsBlock Formats,
    ContactBlock Contact,

    string CitiesIntro,
    List<string> Cities,
    List<string> Keywords,

    CtaBlock Cta
);

public sealed record DuerpIntroBlock(string Title, List<string> Paragraphs);

public sealed record ObjectifBlock(
    string Title,
    List<string> Paragraphs,
    AlertBlock Alert
);
public sealed record AlertBlock(string Title, string Text);

public sealed record ContenuBlock(
    string Title,
    string Lead,
    List<ContentCard> Cards
);
public sealed record ContentCard(string Icon, string Title, string Text);

public sealed record RiskTypesBlock(
    string Title,
    List<RiskItem> Items
);
public sealed record RiskItem(string Icon, string Title, List<string> Bullets);

public sealed record MiseAJourBlock(string Title, List<string> Bullets);

public sealed record FormatsBlock(
    string Title,
    List<FormatItem> Items
);
public sealed record FormatItem(string Icon, string Title, string Text);

