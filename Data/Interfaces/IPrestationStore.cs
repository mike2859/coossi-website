using Coossi.Blazor.Data.Models;

namespace Coossi.Blazor.Data.Interfaces;

public interface IPrestationStore
{
    Task<PrestationDto?> GetBySlugAsync(string slug, CancellationToken ct = default);
    Task<PrestationDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<PrestationDto>> ListAsync(CancellationToken ct = default);
}