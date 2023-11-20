
namespace BaeAiSystem;

using BaeDB;
using BaeDB.Entity;
using BaeIntegrations;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The service for fetching AI systems.
/// </summary>
public class AiSystemService : IAiSystemService
{
    // The integration manager.
    private readonly AiServiceIntegrationManager _integrationManager;
    private readonly BaeDbContext _dbcontext;

    public AiSystemService(AiServiceIntegrationManager integrationManager, BaeDbContext dbcontext)
    {
        _integrationManager = integrationManager;
        _dbcontext = dbcontext;
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
        aiSystems.ForEach(aisystem => _dbcontext.ApprovedAiSystems.Add(new ApprovedAiSystemLinkTable { AiSystemId = string.Empty }));
        await _dbcontext.SaveChangesAsync();
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