using System.Text.Json;
using Coossi.Blazor.Data.Models;

namespace Coossi.Blazor.Data;

// Pages régionales lues une fois au démarrage depuis Content/regions/*.json.
public sealed class RegionStore
{
    private static readonly JsonSerializerOptions _opts =
        new(JsonSerializerDefaults.Web) { ReadCommentHandling = JsonCommentHandling.Skip };

    private readonly Dictionary<string, RegionDto> _parSlug = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, string> _lienParVille = new(StringComparer.OrdinalIgnoreCase);

    public RegionStore(IWebHostEnvironment env)
    {
        var dossier = Path.Combine(env.ContentRootPath, "Content", "regions");
        if (!Directory.Exists(dossier)) return;

        foreach (var fichier in Directory.EnumerateFiles(dossier, "*.json").Order())
        {
            var region = JsonSerializer.Deserialize<RegionDto>(Coordonnees.Remplacer(File.ReadAllText(fichier)), _opts);
            if (region is null || string.IsNullOrWhiteSpace(region.Slug)) continue;

            _parSlug[region.Slug] = region;
            if (region.Brouillon) continue;
            foreach (var ville in region.Villes)
                _lienParVille.TryAdd(ville, Url(region));
        }
    }

    public IEnumerable<RegionDto> Toutes => _parSlug.Values.OrderBy(r => r.Nom);

    // Régions validées : sitemap et liens depuis le reste du site
    public IEnumerable<RegionDto> Publiees => _parSlug.Values.Where(r => !r.Brouillon).OrderBy(r => r.Nom);

    public RegionDto? Get(string slug) => _parSlug.GetValueOrDefault(slug);

    // Lien vers la page régionale d'une ville affichée dans un nuage de villes, s'il en existe une
    public string? LienPourVille(string ville) => _lienParVille.GetValueOrDefault(ville);

    public static string Url(RegionDto region) => $"/coordination-ssi/{region.Slug}";
}
