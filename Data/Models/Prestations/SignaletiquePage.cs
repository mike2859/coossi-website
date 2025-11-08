namespace Coossi.Blazor.Data.Models.Prestations;

public sealed record SigCard(string Title, string? Text = null, List<string>? Bullets = null, string? Icon = null, string? Badge = null);


public sealed record SignaletiquePage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,

    SigIntroBlock Intro,
    SigNormesBlock Normes,
    SigTypeBlock Types,
    SigPictosBlock Pictos,
    SigMateriauxBlock Materiaux,
    SigPrestationBlock Prestation,
    SigEstablishmentsBlock Establishments,
    SigAvantagesBlock Avantages,
    SigContactBlock Contact,

    string CitiesIntro,
    List<string> Cities,

    CtaBlock Cta
);

public sealed record SigIntroBlock(string Title, List<string> Paragraphs);

public sealed record SigNormesBlock(
    string Title,
    string Intro,
    List<string> Normes,
    string Conclusion
);

public sealed record SigTypeBlock(string Title, List<SigTypeItem> Items);
public sealed record SigTypeItem(string Icon, string Title, List<string> Bullets);

public sealed record SigPictosBlock(string Title, List<SigPictoItem> Items);
public sealed record SigPictoItem(string Icon, string Label, string Color);

public sealed record SigMateriauxBlock(string Title, List<SigMateriauxItem> Items);
public sealed record SigMateriauxItem(string Icon, string Title, string Text);

public sealed record SigPrestationBlock(
    string Title,
    string Subtitle,
    SigPrestationColumn Etude,
    SigPrestationColumn Realisation
);
public sealed record SigPrestationColumn(string Title, List<string> Bullets);

public sealed record SigEstablishmentsBlock(string Title, List<SigEstItem> Items);
public sealed record SigEstItem(string Icon, string Title, string Small);

public sealed record SigAvantagesBlock(
    string Title,
    List<string> LeftBullets,
    List<string> RightBullets
);

public sealed record SigContactBlock(
    string Title,
    string Text,
    string PhoneDisplay,
    string PhoneHref,
    string ButtonText,
    string ButtonHref
);