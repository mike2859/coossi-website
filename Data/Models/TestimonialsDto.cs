#nullable enable
namespace Coossi.Blazor.Data.Models;

public record TestimonialsPage(string Title, string? Lead, List<TestimonialItem> Items);
public record TestimonialItem(int Rating, string Content, string ClientName, string? ClientCompany);
