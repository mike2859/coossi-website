#nullable enable
namespace Coossi.Blazor.Data.Models;

public record ContactPage(
    string Id,
    string Title,
    string Lead,
    ContactInfo ContactInfo,
    ContactForm Form,
    ContactMap Map,
    ContactZone Zone,
    ContactFaq Faq
);

public record ContactInfo(
    PhoneInfo Phone,
    EmailInfo Email,
    LocationInfo Location,
    HoursInfo Hours
);

public record PhoneInfo(string Icon, string Title, string Number, string Href, string Note);
public record EmailInfo(string Icon, string Address, string Href, string Note);
public record LocationInfo(string Icon, string City, string Region, string Note);
public record HoursInfo(string Icon, List<string> Lines, string Note);

public record ContactForm(
    string Title,
    string SubmitText,
    string SuccessText,
    List<ContactField> Fields,
    string FallbackPhoneText,
    string FallbackPhone,
    string FallbackPhoneHref
);

public record ContactField(
    string Type,          // "text" | "email" | "tel" | "select" | "textarea" | "checkbox"
    string Id,
    string Label,
    bool Required,
    int? Rows,
    List<string>? Options
);

public record ContactMap(string Title, string Subtitle, string Small, string Icon);

public record ContactZone(string Title, ZoneAreas Areas, string Note);

public record ZoneAreas(List<string> Principale, List<string> Villes);

public record ContactFaq(string Title, string Lead, List<FaqItem> Items);

public record FaqItem(string Icon, string Q, string A);
