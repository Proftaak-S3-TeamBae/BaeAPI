
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

        var approvedSystems = aiSystems.Select(aiSystem => new FetchedAiSystem
        {
            Name = aiSystem.Name,
            Type = aiSystem.Type,
            Source = aiSystem.Source,
            Description = aiSystem.Description,
            DateAdded = aiSystem.DateAdded
        }).ToList();

        await _integrationManager.ApproveAiSystemsAsync(approvedSystems);
    }

    /// <summary>
    /// Add AI systems to the list of disapproved systems
    /// </summary>
    /// <param name="aiSystems">The AI systems to add.</param>
    public async Task DisapproveAiSystemsAsync(List<AiSystem> aiSystems)
    {
        var disapprovedSystems = aiSystems.Select(aiSystem => new FetchedAiSystem
        {
            Name = aiSystem.Name,
            Type = aiSystem.Type,
            Source = aiSystem.Source,
            Description = aiSystem.Description,
            DateAdded = aiSystem.DateAdded
        }).ToList();

        await _integrationManager.DisapproveAiSystemsAsync(disapprovedSystems);
    }       
}