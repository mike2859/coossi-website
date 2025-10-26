namespace Coossi.Blazor.Data.Models.Prestations;

public sealed record DossierSsiPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,
    string IntroText,
    string NormeText,
    List<PhaseReception> PhaseReceptions,   // Vérification/Essais/Formation/Dossier
    string ObligationText,
    string BureauEtudesText,
    List<string> CcfPoints,        // ex: Catégorie SSI, corrélations, AES/APS, DCT/DAS, réception
    string CoherenceText,
    string ContactText,
    List<string> Cities,
    CtaBlock Cta
);

public sealed record PhaseReception(string Title, string Text);