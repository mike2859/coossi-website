namespace Coossi.Blazor.Data.Models;

// Page « Coordination SSI en <région> » : un fichier Content/regions/<slug>.json par région.
public sealed class RegionDto
{
    public string Slug { get; set; } = "";
    // Brouillon : page accessible par son adresse mais en noindex, hors sitemap et sans lien sur le site
    public bool Brouillon { get; set; } = true;
    public string Nom { get; set; } = "";
    // Avec la bonne préposition : « en Normandie », « dans les Hauts-de-France »
    public string Lieu { get; set; } = "";
    // Région du siège (Villers-Pol) : pas de section « intervention à distance »
    public bool Siege { get; set; }
    public string MetaTitle { get; set; } = "";
    public string MetaDescription { get; set; } = "";
    public string Titre { get; set; } = "";
    public string SousTitre { get; set; } = "";
    public List<string> Intro { get; set; } = [];
    public List<string> Villes { get; set; } = [];
    public List<DepartementDto> Departements { get; set; } = [];
    public List<string> Specificites { get; set; } = [];
    // Missions réalisées dans la région (la section n'apparaît que si la liste est remplie)
    public List<MissionRegionDto> Missions { get; set; } = [];
}

public sealed class DepartementDto
{
    public string Numero { get; set; } = "";
    public string Nom { get; set; } = "";
}

public sealed class MissionRegionDto
{
    public string Etablissement { get; set; } = "";
    public string Ville { get; set; } = "";
    public string Annee { get; set; } = "";
    public string Description { get; set; } = "";
}
