
namespace BaeAiSystem;

using BaeIntegrations;

/// <summary>
/// The service for fetching AI systems.
/// </summary>
public class AiSystemService : IAiSystemService
{
    // The integration manager.
    private readonly AiServiceIntegrationManager _integrationManager;

    public AiSystemService(AiServiceIntegrationManager integrationManager)
    {
        _integrationManager = integrationManager;
    }

    public async Task<List<AiSystem>> GetAiSystemsAsync()
        => (await _integrationManager.GetAiSystemsAsync()).Select(aiSystem => new AiSystem
        {
            Name = aiSystem.Name,
            Type = aiSystem.Type,
            Source = aiSystem.Source,
            Description = aiSystem.Description,
            DateAdded = aiSystem.DateAdded
        }).ToList();

    public Task ApproveAiSystemsAsync(List<AiSystem> aiSystems)
    {
        // TODO: Implement once the database is implemented.
        throw new NotImplementedException();
    }

    public Task DisapproveAiSystemsAsync(List<AiSystem> aiSystems)
    {
        throw new NotImplementedException();
    }

}