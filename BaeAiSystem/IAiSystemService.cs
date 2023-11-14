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

    /// <summary>
    /// Add AI systems to the list of approved systems
    /// </summary>
    /// <param name="aiSystems">The AI systems to add.</param>
    public Task ApproveAiSystemsAsync(List<AiSystem> aiSystems)
    
    /// <summary>
    /// Add AI systems to the list of disapproved systems
    /// </summary>
    /// <param name="aiSystems">The AI systems to add.</param>
    public Task DisapproveAiSystemsAsync(List<AiSystem> aiSystems)
}