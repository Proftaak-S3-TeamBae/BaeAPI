using BaeDB;

namespace BaeIntegrations;

/// <summary>
/// The manager for the AI service integrations.
/// </summary>
public class AiServiceIntegrationManager
{
    private readonly BaeDbContext _dbContext;

    public AiServiceIntegrationManager(BaeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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
            integration.Initialize(_dbContext);
            var fetchedAiSystems = await integration.GetAiSystemsAsync();
            aiSystems.AddRange(fetchedAiSystems);
        }

        return aiSystems;
    }
}