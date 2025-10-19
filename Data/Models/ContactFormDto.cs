using System.ComponentModel.DataAnnotations;

namespace Coossi.Blazor.Data.Models;

public class ContactFormDto
{
    [Required(ErrorMessage = "Votre nom est requis.")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Votre email est requis.")]
    [EmailAddress(ErrorMessage = "Format d’email invalide.")]
    public string? Email { get; set; }

    [Phone(ErrorMessage = "Format de téléphone invalide.")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Merci d’indiquer un sujet.")]
    [StringLength(140)]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Votre message est requis.")]
    [MinLength(10, ErrorMessage = "10 caractères minimum.")]
    public string? Message { get; set; }

    // anti-bot (honeypot)
    public string? Company { get; set; }
    public bool Consent { get; set; } = true;
}
