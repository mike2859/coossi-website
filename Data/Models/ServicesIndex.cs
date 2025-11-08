#nullable enable
namespace Coossi.Blazor.Data.Models;

public record ServiceItem(string Title, string Slug, string? Icon, string? Excerpt, List<string>? Bullets);
public record ServicesIndex(List<ServiceItem> Items);
