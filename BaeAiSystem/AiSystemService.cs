
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
            Integration = aiSystem.Integration,
            Purpose = aiSystem.Purpose,
            DateAdded = aiSystem.DateAdded
        }).ToList();

    public async Task ApproveAiSystemsAsync(List<AiSystem> aiSystems)
    {
        aiSystems.ForEach(aisystem =>
        {
            string id = aisystem.GenerateId();
            // Check if the AI system already exists.
            if (_dbcontext.AiSystems.Any(aiSystem => aiSystem.Id == id))
                return;
            _dbcontext.AiSystems.Add(new AiSystemEntity
            {
                Id = id,
                Name = aisystem.Name,
                Type = aisystem.Type,
                Integration = aisystem.Integration,
                Purpose = aisystem.Purpose,
                DateAdded = aisystem.DateAdded
            });
        });
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