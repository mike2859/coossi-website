using System.ComponentModel.DataAnnotations;

namespace Coossi.Blazor.Data.Models;

public class ContactFormModel
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le prénom est requis")]
    [StringLength(100, ErrorMessage = "Le prénom ne peut pas dépasser 100 caractères")]
    public string Prenom { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le téléphone est requis")]
    [Phone(ErrorMessage = "Format de téléphone invalide")]
    public string Telephone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le type de projet est requis")]
    public string TypeProjet { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le message est requis")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Le message doit contenir entre 10 et 2000 caractères")]
    public string Message { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vous devez accepter la politique de confidentialité")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Vous devez accepter la politique de confidentialité")]
    public bool AccepteConfidentialite { get; set; }

    // // anti-bot (honeypot)
    // public string? Company { get; set; }
    // public bool Consent { get; set; } = true;
}
