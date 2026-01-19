using Coossi.Blazor.Data.Models;

namespace Coossi.Blazor.Data.Interfaces;

public interface IEmailService
{
    Task<bool> SendContactEmailAsync(ContactFormModel model);
}