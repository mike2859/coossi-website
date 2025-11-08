namespace Coossi.Blazor.Data.Interfaces;

public interface IContentStore
{
    /// <summary>
    /// Charge un JSON depuis Content/{subfolder?}/{id}.json et le désérialise en T.
    /// </summary>
    Task<T?> GetAsync<T>(string id, string? subfolder = null, CancellationToken ct = default);
    
    Task<string?> GetRawAsync(string id, string? subfolder = null, CancellationToken ct = default);
}
