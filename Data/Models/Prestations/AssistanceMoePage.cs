namespace Coossi.Blazor.Data.Models.Prestations;


public sealed record AssistanceMoePage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,
    string MissionText,
    List<CardBlock> MissionsCoordinateur,
    string SuiviRealisationText,
    List<string> InterventionsPoints,
    string BureauEtudesText,
    string QualifText,
    List<CardBlock> Expertises,
    List<string> PlansDocs,
    string ContactText,
    List<string> Cities,
    CtaBlock Cta
);
