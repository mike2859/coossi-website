
using System.Text.Json;
using Coossi.Blazor.Data.Interfaces;

namespace Coossi.Blazor.Data;

public sealed class FileContentStore : IContentStore
{
    private readonly string _root;
    private static readonly JsonSerializerOptions _opts =
        new(JsonSerializerDefaults.Web) { ReadCommentHandling = JsonCommentHandling.Skip };

    public FileContentStore(IWebHostEnvironment env)
        => _root = Path.Combine(env.ContentRootPath, "Content");

    public async Task<T?> GetAsync<T>(string id, string? subfolder = null, CancellationToken ct = default)
    {
        var folder = string.IsNullOrWhiteSpace(subfolder) ? _root : Path.Combine(_root, subfolder);
        var file = Path.Combine(folder, id + ".json");
        if (!File.Exists(file)) return default;

        var json = await File.ReadAllTextAsync(file, ct);
        if (string.IsNullOrWhiteSpace(json)) return default;

        try { return JsonSerializer.Deserialize<T>(json, _opts); }
        catch { return default; }
    }
}