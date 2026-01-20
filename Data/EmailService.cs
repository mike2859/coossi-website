using Coossi.Blazor.Data.Interfaces;
using Coossi.Blazor.Data.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Coossi.Blazor.Data;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    
    public async Task<bool> SendContactEmailAsync(ContactFormModel model)
    {
        try
        {
            var message = new MimeMessage();
            
            // Expéditeur (l'email configuré dans appsettings)
            var fromAddress = _configuration["EmailSettings:FromEmail"] ?? "noreply@coossi.fr";
            var fromName = _configuration["EmailSettings:Username"] ?? "COOSSI Contact";
            message.From.Add(new MailboxAddress(fromName, fromAddress));

            // DESTINATAIRES MULTIPLES - Les 3 emails
            var recipients = _configuration.GetSection("EmailSettings:Recipients").Get<string[]>() 
                    ?? new[] { "mickael.leblanc59@gmail.com" };

                foreach (var recipient in recipients)
                {
                    message.To.Add(MailboxAddress.Parse(recipient));
                }

            // Ajouter l'email du client en ReplyTo pour faciliter la réponse
            message.ReplyTo.Add(new MailboxAddress($"{model.Prenom} {model.Nom}", model.Email));

            // Sujet
            message.Subject = $"Nouveau contact : {model.TypeProjet} - {model.Nom} {model.Prenom}";

            // Corps du message
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = GenerateEmailBody(model)
            };

            message.Body = bodyBuilder.ToMessageBody();

            // Envoi via SMTP
            using var client = new SmtpClient();
            
            var smtpHost = _configuration["EmailSettings:Host"];
            var smtpPort = int.Parse(_configuration["EmailSettings:Port"] ?? "587");
            var smtpUser = _configuration["EmailSettings:Username"];
            var smtpPass = _configuration["EmailSettings:Password"];

            await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(smtpUser, smtpPass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email de contact envoyé avec succès pour {Email} vers {Count} destinataires", 
                model.Email, recipients.Length);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'envoi de l'email de contact pour {Email}", model.Email);
            return false;
        }
    }
      

        private string GenerateEmailBody(ContactFormModel model)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #dc3545; color: white; padding: 20px; text-align: center; }}
        .content {{ background-color: #f8f9fa; padding: 20px; margin-top: 20px; }}
        .field {{ margin-bottom: 15px; }}
        .label {{ font-weight: bold; color: #dc3545; }}
        .value {{ margin-top: 5px; padding: 10px; background-color: white; border-left: 3px solid #dc3545; }}
        .footer {{ text-align: center; margin-top: 20px; color: #6c757d; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h2>Nouvelle demande de contact - COOSSI</h2>
        </div>
        <div class='content'>
            <div class='field'>
                <div class='label'>Nom complet :</div>
                <div class='value'>{model.Prenom} {model.Nom}</div>
            </div>
            <div class='field'>
                <div class='label'>Email :</div>
                <div class='value'><a href='mailto:{model.Email}'>{model.Email}</a></div>
            </div>
            <div class='field'>
                <div class='label'>Téléphone :</div>
                <div class='value'><a href='tel:{model.Telephone}'>{model.Telephone}</a></div>
            </div>
            <div class='field'>
                <div class='label'>Type de projet :</div>
                <div class='value'>{model.TypeProjet}</div>
            </div>
            <div class='field'>
                <div class='label'>Message :</div>
                <div class='value'>{model.Message.Replace("\n", "<br/>")}</div>
            </div>
        </div>
        <div class='footer'>
            <p>Ce message a été envoyé depuis le formulaire de contact du site COOSSI</p>
            <p>Date : {DateTime.Now:dd/MM/yyyy HH:mm}</p>
        </div>
    </div>
</body>
</html>";
    }
}