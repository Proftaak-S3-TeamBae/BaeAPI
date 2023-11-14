namespace BaeIntegrations;

/// <summary>
/// The manager for the AI service integrations.
/// </summary>
public class AiServiceIntegrationManager
{
    /// <summary>
    /// The registry of the AI service integrations.
    /// </summary>
    private Dictionary<string, IAiServiceIntegration> _integrations { get; set; } = new();

    /// <summary>
    /// Register an AI service integration.
    /// </summary>
    /// <typeparam name="T">The integration</typeparam>
    public void RegisterIntegration<T>(T serviceIntegration) where T : IAiServiceIntegration
        => _integrations.Add(serviceIntegration.Id, serviceIntegration);

    /// <summary>
    /// Get an AI service integration.
    /// </summary>
    /// <param name="id">The id of the integration.</param>
    /// <returns>The integration.</returns>
    public IAiServiceIntegration GetIntegration(string id)
        => _integrations[id];

    /// <summary>
    /// Get all AI systems from all integrations.
    /// </summary>
    /// <returns>The AI systems.</returns>
    public async Task<List<FetchedAiSystem>> GetAiSystemsAsync()
    {
        var aiSystems = new List<FetchedAiSystem>();
        foreach (var integration in _integrations.Values)
        {
            var fetchedAiSystems = await integration.GetAiSystemsAsync();
            aiSystems.AddRange(fetchedAiSystems);
        }

        return aiSystems;
    }

    /// <summary>
    /// Add AI systems to the list of approved systems
    /// </summary>
    /// <param name="aiSystems">The AI systems to add.</param>
    public async Task ApproveAiSystemsAsync(List<FetchedAiSystem> aiSystems)
    {
        throw new NotImplementedException(); // TODO: Implement when database connection added
    }

    /// <summary>
    /// Add AI systems to the list of disapproved systems
    /// </summary>
    /// <param name="aiSystems">The AI systems to add.</param>
    public async Task DisapproveAiSystemsAsync(List<FetchedAiSystem> aiSystems)
    {
        throw new NotImplementedException(); // TODO: Implement when database connection added
    } Task DisapprovedAiSystemsAsync(List<FetchedAiSystem> aisystems)
    {
        throw new NotImplementedException(); // TODO: Implement when database connection added
    }
}