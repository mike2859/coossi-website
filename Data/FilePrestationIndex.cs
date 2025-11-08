using System.Text.Json;
using Coossi.Blazor.Data.Interfaces;

namespace Coossi.Blazor.Data;

public class FilePrestationIndex :  IPrestationIndex
{
    private readonly string _root;

    public FilePrestationIndex(IWebHostEnvironment env)
        => _root = Path.Combine(env.ContentRootPath, "Content", "prestations");

    public async Task<string?> GetSlugByIdAsync(int id, CancellationToken ct = default)
    {
        foreach (var path in Directory.EnumerateFiles(_root, "*.json", SearchOption.TopDirectoryOnly))
        {
            await using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var doc = await JsonDocument.ParseAsync(fs, cancellationToken: ct);

            var root = doc.RootElement;
            if (!root.TryGetProperty("Id", out var idEl)) continue;
            if (idEl.ValueKind != JsonValueKind.Number) continue;

            if (idEl.GetInt32() == id)
            {
                if (root.TryGetProperty("Slug", out var slugEl) && slugEl.ValueKind == JsonValueKind.String)
                    return slugEl.GetString();
            }
        }
        return null;
    }
}