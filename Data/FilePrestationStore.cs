using System.Text.Json;
using Coossi.Blazor.Data.Interfaces;
using Coossi.Blazor.Data.Models;

namespace Coossi.Blazor.Data;

public sealed class FilePrestationStore : IPrestationStore
{
    private readonly string _root;
    private readonly JsonSerializerOptions _opts = new(JsonSerializerDefaults.Web) { WriteIndented = true };

    public FilePrestationStore(IWebHostEnvironment env)
        => _root = Path.Combine(env.ContentRootPath, "Content", "prestations");

    public async Task<PrestationDto?> GetBySlugAsync(string slug, CancellationToken ct = default)
    {
        var file = Path.Combine(_root, slug + ".json");
        return File.Exists(file)
            ? JsonSerializer.Deserialize<PrestationDto>(await File.ReadAllTextAsync(file, ct), _opts)
            : null;
    }

    public async Task<PrestationDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        foreach (var f in Directory.EnumerateFiles(_root, "*.json"))
        {
            var dto = JsonSerializer.Deserialize<PrestationDto>(await File.ReadAllTextAsync(f, ct), _opts);
            if (dto?.Id == id) return dto;
        }
        return null;
    }

    public async Task<IReadOnlyList<PrestationDto>> ListAsync(CancellationToken ct = default)
    {
        var list = new List<PrestationDto>();
        foreach (var f in Directory.EnumerateFiles(_root, "*.json"))
        {
            var dto = JsonSerializer.Deserialize<PrestationDto>(await File.ReadAllTextAsync(f, ct), _opts);
            if (dto is not null) list.Add(dto);
        }
        return list.OrderBy(p => p.Title).ToList();
    }
}