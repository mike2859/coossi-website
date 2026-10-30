namespace Coossi.Blazor.Data;

// Coordonnées affichées sur tout le site : c'est le seul endroit à modifier.
// Pages Razor : @Coordonnees.Telephone, @Coordonnees.TelephoneLien.
// Fichiers Content/*.json : étiquettes {{TEL}}, {{TEL_LIEN}}, {{TEL_INTL}}, remplacées à la lecture.
public static class Coordonnees
{
    public const string Telephone = "06 67 28 35 95";
    public const string TelephoneLien = "tel:0667283595";
    public const string TelephoneInternational = "+33667283595";

    public static string Remplacer(string texte) => texte
        .Replace("{{TEL_LIEN}}", TelephoneLien)
        .Replace("{{TEL_INTL}}", TelephoneInternational)
        .Replace("{{TEL}}", Telephone);
}
