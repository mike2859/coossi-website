
using Coossi.Blazor.Data.Models;

namespace Coossi.Blazor.Data.Interfaces;

public interface ILegalStore
{
    Task<LegalPageDto?> GetAsync(string id, CancellationToken ct = default);
}