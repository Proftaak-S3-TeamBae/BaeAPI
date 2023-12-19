using BaeDB;
using BaeIntegrations.Exceptions;
using Microsoft.Extensions.Logging;

namespace BaeIntegrations;

/// <summary>
/// The manager for the AI service integrations.
/// </summary>
public class AiServiceIntegrationManager
{
    private readonly ILogger _logger;


    public AiServiceIntegrationManager(ILogger<AiServiceIntegrationManager> logger)
    {
        _logger = logger;
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
    /// Get an AI service integration.
    /// </summary>
    /// <typeparam name="T">The integration type.</typeparam>
    /// <returns>The integration.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the integration is not found.</exception>
    public T GetIntegration<T>() where T : IAiServiceIntegration
        => (T)_integrations.First(integration => integration.Value.GetType() == typeof(T)).Value;

    /// <summary>
    /// Get all AI systems from all integrations.
    /// </summary>
    /// <returns>The AI systems.</returns>
    public async Task<List<FetchedAiSystem>> GetAiSystemsAsync()
    {
        var aiSystems = new List<FetchedAiSystem>();
        foreach (var integration in _integrations.Values)
        {
            try
            {
                var fetchedAiSystems = await integration.GetAiSystemsAsync();
                aiSystems.AddRange(fetchedAiSystems);
            }
            catch (IntegrationUnhandledException) { }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to get AI systems from {integration.Id}");
            }
        }

        return aiSystems;
    }
}