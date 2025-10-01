namespace Coossi.Blazor.Data.Models;

public record KeywordItem(string Text, int Weight = 1, string? Url = null, string? Title = null);
public record KeywordCloudDto(string Id, string? Title, List<KeywordItem> Items);
