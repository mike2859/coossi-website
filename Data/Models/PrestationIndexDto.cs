namespace Coossi.Blazor.Data.Models;


public record PrestationItem(string Title, string Slug, string? Icon, string? Excerpt, List<string>? Bullets);

public record PrestationsIndex(List<PrestationItem> Items);