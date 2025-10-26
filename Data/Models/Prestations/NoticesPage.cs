namespace Coossi.Blazor.Data.Models.Prestations;

public sealed record NoticesPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,
    string IntroText,
    List<CardBlock> Notices,          // deux cartes (sécurité / accessibilité)
    string IntegrationText,           // intégration au dossier SSI
    List<string> PlansZones,          // ZA, ZC, ZF, ZDA, ZDM
    List<CardBlock> DocsComplement,   // plans SDI/SMSI, synoptiques, etc.
    List<CardBlock> TypesEtablissements,
    string ContactText,
    List<string> Cities,
    CtaBlock Cta
);