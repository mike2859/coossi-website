namespace Coossi.Blazor.Data.Interfaces;

public interface IPrestationIndex
{
    Task<string?> GetSlugByIdAsync(int id, CancellationToken ct = default);
    // (optionnel) Task<IReadOnlyDictionary<int,string>> GetAllIdToSlugAsync(CancellationToken ct = default);
}