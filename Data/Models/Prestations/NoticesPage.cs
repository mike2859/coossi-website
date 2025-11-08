namespace Coossi.Blazor.Data.Models.Prestations;


public sealed record NoticesPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,

    IntroBlock Intro,
    DocsPermisBlock DocsPermis,
    NoticesBlock Notices,
    IntegrationBlock Integration,
    PlansBlock Plans,
    DocsComplementBlock DocsComplement,
    EstablishmentsBlock Establishments,
    ContactBlock Contact,

    string CitiesIntro,
    List<string> Cities,

    CtaBlock Cta
);

public sealed record IntroBlock(string Title, List<string> Paragraphs);

public sealed record DocsPermisBlock(string Title, List<string> Paragraphs);

public sealed record NoticesBlock(string Title, List<NoticeItem> Items);
public sealed record NoticeItem(string Badge, string Title, string Text, List<string> Bullets);

public sealed record IntegrationBlock(string Title, string Text);

public sealed record PlansBlock(string Title, string Intro, List<string> Items);

public sealed record DocsComplementBlock(string Title, List<DocItem> Items, string Conclusion);
public sealed record DocItem(string Icon, string Title, string Text);

public sealed record EstablishmentsBlock(string Title, List<EstablishmentItem> Items);
public sealed record EstablishmentItem(string Icon, string Title, string Subtitle, string Small);
