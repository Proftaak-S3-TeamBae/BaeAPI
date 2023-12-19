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
    /// Approve AI systems.
    /// </summary>
    /// <param name="aiSystems">The AI systems to approve.</param>
    /// <returns></returns>
    public Task ApproveAiSystemsAsync(List<AiSystem> aiSystems);

    /// <summary>
    /// Get the list of approved AI systems.
    /// </summary>
    /// <returns>The approved AI systems.</returns>
    public Task<List<AiSystem>> GetApprovedAiSystemsAsync();

    /// <summary>
    /// Get the list of disapproved AI systems.
    /// </summary>
    /// <returns>The disapproved AI systems.</returns>
    public Task<List<AiSystem>> GetDisapprovedAiSystemsAsync();

    /// <summary>
    /// Disapprove AI systems.
    /// </summary>
    /// <param name="aiSystems">The AI systems to disapprove.</param>
    /// <returns></returns>
    public Task DisapproveAiSystemsAsync(List<AiSystem> aiSystems);

    /// <summary>
    /// Remove AI systems.
    /// </summary>
    /// <param name="aiSystems">The AI systems to remove.</param>
    /// <returns></returns>
    public Task RemoveAiSystemsAsync(List<AiSystem> aiSystems);
}