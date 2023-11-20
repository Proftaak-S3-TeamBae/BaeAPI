
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

    public async Task ApproveAiSystemsAsync(List<AiSystem> aiSystems)
    {

    }

    public async Task<List<AiSystem>> GetApprovedAiSystemsAsync()
    {
        return new();
    }

    public async Task DisapproveAiSystemsAsync(List<AiSystem> aiSystems)
    {

    }

    public async Task<List<AiSystem>> GetDisapprovedAiSystemsAsync()
    {
        return new();
    }
}