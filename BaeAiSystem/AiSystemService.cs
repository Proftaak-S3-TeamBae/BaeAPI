
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
}

// Post Approved list Must have
// Post Disapproved list Should have
// Get Approved list for user