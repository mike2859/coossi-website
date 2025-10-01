using System.Text.Json;
using Coossi.Blazor.Data.Interfaces;
using Coossi.Blazor.Data.Models;


namespace Coossi.Blazor.Data;

public sealed class FileKeywordStore : IKeywordStore
{
    private readonly string _root;
    private readonly JsonSerializerOptions _opts = new(JsonSerializerDefaults.Web);
    public FileKeywordStore(IWebHostEnvironment env)
        => _root = Path.Combine(env.ContentRootPath, "Content", "keywords");

    public async Task<KeywordCloudDto?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var file = Path.Combine(_root, id + ".json");
        if (!File.Exists(file)) return null;

        var json = await File.ReadAllTextAsync(file, ct);
        if (string.IsNullOrWhiteSpace(json)) return null;

        try { return JsonSerializer.Deserialize<KeywordCloudDto>(json, _opts); }
        catch { return null; }
    }

    public Task<string[]> ListIdsAsync(CancellationToken ct = default)
        => Task.FromResult(Directory.Exists(_root)
            ? Directory.GetFiles(_root, "*.json").Select(Path.GetFileNameWithoutExtension).ToArray()
            : Array.Empty<string>());

  
}