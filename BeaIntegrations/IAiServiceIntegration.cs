using BaeDB;

namespace BaeIntegrations;

/// <summary>
/// An integration for an AI service.
/// </summary>
public interface IAiServiceIntegration
{
    /// <summary>
    /// The id of the integration.
    /// </summary>
    public string Id { get; protected set; }

    /// <summary>
    /// Get all AI systems from the integration.
    /// </summary>
    /// <returns>The AI systems.</returns>
    /// <exception cref="NotImplementedException">Thrown if the method is not implemented.</exception>
    public Task<List<FetchedAiSystem>> GetAiSystemsAsync();
}