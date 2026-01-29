namespace Coossi.Blazor.Data.Models;

public record PartnersPage(string Title, string? Lead, List<PartnerItem> Items);
public record PartnerItem(string Icon, string Name, string? Description);
