namespace BaeAiSystem;

/// <summary>
/// The service for fetching AI systems.
/// </summary>
public interface IAiSystemService
{
    /// <summary>
    /// Get all AI systems.
    /// </summary>
    /// <returns>The AI systems.</returns>
    public Task<List<AiSystem>> GetAiSystemsAsync();
}