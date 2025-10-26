namespace Coossi.Blazor.Data.Models.Prestations;

public sealed record SigCard(string Title, string? Text = null, List<string>? Bullets = null, string? Icon = null, string? Badge = null);

public sealed record SignaletiquePage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,
    string ImportanceText,
    string ConformiteText,
    List<SigCard> PlansEvacuation,
    List<SigCard> Directionnels,
    List<SigCard> Equipements,
    List<SigCard> Interdictions,
    List<SigCard> Consignes,
    List<SigCard> Balisage,
    List<SigCard> Pictos,
    List<SigCard> MateriauxSupports,
    string EtudeConceptionText,
    string RealisationPoseText,
    List<string> Etablissements,
    List<string> Cities,
    CtaBlock Cta
);