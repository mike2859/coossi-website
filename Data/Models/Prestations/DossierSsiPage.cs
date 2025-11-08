namespace Coossi.Blazor.Data.Models.Prestations;


public sealed record DossierSsiPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,

    DossierSSIIntroBlock Intro,
    DossierSSILegalBlock Legal,
    ContenuDossierBlock ContenuDossier,
    DossierSSIMethodoBlock Methodo,
    DossierSSIEstablishmentsBlock Establishments,
    DossierSSIAdvantagesBlock Advantages,
    DossierSSIContactBlock Contact,

    // on garde ta section existante Zones d'intervention
    string CitiesIntro,
    List<string> Cities,
    List<string> Keywords,

    CtaBlock Cta
);


public sealed record DossierSSIIntroBlock(string Title, List<string> Paragraphs);

public sealed record DossierSSILegalBlock(
    string Title,
    List<string> Paragraphs,
    string HighlightTitle,
    string HighlightText
);

public sealed record ContenuDossierBlock(
    string Title,
    string Lead,
    ContenuSide Left,
    ContenuSide Right
);
public sealed record ContenuSide(string Title, string Icon, List<string> Bullets);

public sealed record DossierSSIMethodoBlock(
    string Title,
    string Lead,
    List<MethodStep> Steps
);
public sealed record DossierSSIMethodStep(string Title, string Icon, string Text);

public sealed record DossierSSIEstablishmentsBlock(
    string Title,
    List<DossierSSIEstablishmentItem> Items
);
public sealed record DossierSSIEstablishmentItem(string Title, string Icon, string Subtitle, string Small);

public sealed record DossierSSIAdvantagesBlock(
    string Title,
    List<string> LeftBullets,
    List<string> RightBullets
);

public sealed record DossierSSIContactBlock(
    string Title,
    string Text,
    string PhoneDisplay,
    string PhoneHref,
    string ButtonText,
    string ButtonHref
);