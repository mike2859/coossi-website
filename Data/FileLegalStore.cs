using System.Text.Json;
using Coossi.Blazor.Data.Interfaces;
using Coossi.Blazor.Data.Models;

namespace Coossi.Blazor.Data;

public sealed class FileLegalStore : ILegalStore
{
    private readonly string _root;
    private readonly JsonSerializerOptions _opts = new(JsonSerializerDefaults.Web) { ReadCommentHandling = JsonCommentHandling.Skip };

    public FileLegalStore(IWebHostEnvironment env)
        => _root = Path.Combine(env.ContentRootPath, "Content", "legal");

    public async Task<LegalPageDto?> GetAsync(string id, CancellationToken ct = default)
    {
        var file = Path.Combine(_root, id + ".json");
        if (!File.Exists(file)) return null;

        var json = await File.ReadAllTextAsync(file, ct);
        if (string.IsNullOrWhiteSpace(json)) return null;

        try { return JsonSerializer.Deserialize<LegalPageDto>(json, _opts); }
        catch { return null; }
    }
}