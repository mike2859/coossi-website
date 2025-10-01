using Coossi.Blazor.Data.Models;

namespace Coossi.Blazor.Data.Interfaces;

public interface IKeywordStore
{
    Task<KeywordCloudDto?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<string[]> ListIdsAsync(CancellationToken ct = default);
}