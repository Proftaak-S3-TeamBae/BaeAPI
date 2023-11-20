
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
            Version = aiSystem.Version,
            Purpose = aiSystem.Purpose,
            DateAdded = aiSystem.DateAdded
        }).ToList();

    public async Task ApproveAiSystemsAsync(List<AiSystem> aiSystems)
    {
        aiSystems.ForEach(aisystem =>
        {
            string id = aisystem.GenerateId();
            // Check if the AI system already exists.
            if (!_dbcontext.AiSystems.Any(aiSystem => aiSystem.Id == id))
            {
                _dbcontext.AiSystems.Add(new AiSystemEntity
                {
                    Id = id,
                    Name = aisystem.Name,
                    Type = aisystem.Type,
                    Version = aisystem.Version,
                    Integration = aisystem.Integration,
                    Purpose = aisystem.Purpose,
                    DateAdded = aisystem.DateAdded
                });
            }


            // Add the system to the approved systems.
            _dbcontext.ApprovedAiSystems.Add(new ApprovedAiSystemLinkTable
            {
                Id = Guid.NewGuid().ToString(),
                AiSystemId = id
            });

            // Remove it from the disapproved systems if it exists.
            var disapprovedSystem = _dbcontext.DisapprovedAiSystems.FirstOrDefault(disapprovedSystem => disapprovedSystem.AiSystemId == id);
            if (disapprovedSystem != null)
                _dbcontext.DisapprovedAiSystems.Remove(disapprovedSystem);
        });
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<List<AiSystem>> GetApprovedAiSystemsAsync()
    {
        var result = new List<AiSystem>();
        var approvedSystems = await _dbcontext.ApprovedAiSystems.ToListAsync();
        approvedSystems.ForEach(approvedSystem =>
        {
            var aiSystem = _dbcontext.AiSystems.FirstOrDefault(aiSystem => aiSystem.Id == approvedSystem.AiSystemId);
            if (aiSystem != null)
                result.Add(new AiSystem
                {
                    Name = aiSystem.Name,
                    Type = aiSystem.Type,
                    Version = aiSystem.Version,
                    Integration = aiSystem.Integration,
                    Purpose = aiSystem.Purpose,
                    DateAdded = aiSystem.DateAdded
                });
        });
        return result;
    }

    public async Task DisapproveAiSystemsAsync(List<AiSystem> aiSystems)
    {
        aiSystems.ForEach(aisystem =>
        {
            string id = aisystem.GenerateId();
            // Check if the AI system already exists.
            if (!_dbcontext.AiSystems.Any(aiSystem => aiSystem.Id == id))
            {
                _dbcontext.AiSystems.Add(new AiSystemEntity
                {
                    Id = id,
                    Name = aisystem.Name,
                    Type = aisystem.Type,
                    Version = aisystem.Version,
                    Integration = aisystem.Integration,
                    Purpose = aisystem.Purpose,
                    DateAdded = aisystem.DateAdded
                });
            }

            // Add the system to the disapproved systems.
            _dbcontext.DisapprovedAiSystems.Add(new DisapprovedAiSystemLinkTable
            {
                Id = Guid.NewGuid().ToString(),
                AiSystemId = id
            });

            // Remove it from the approved systems if it exists.
            var approvedSystem = _dbcontext.ApprovedAiSystems.FirstOrDefault(approvedSystem => approvedSystem.AiSystemId == id);
            if (approvedSystem != null)
                _dbcontext.ApprovedAiSystems.Remove(approvedSystem);
        });
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<List<AiSystem>> GetDisapprovedAiSystemsAsync()
    {
        var result = new List<AiSystem>();
        var disapprovedSystems = await _dbcontext.DisapprovedAiSystems.ToListAsync();
        disapprovedSystems.ForEach(disapprovedSystem =>
        {
            var aiSystem = _dbcontext.AiSystems.FirstOrDefault(aiSystem => aiSystem.Id == disapprovedSystem.AiSystemId);
            if (aiSystem != null)
                result.Add(new AiSystem
                {
                    Name = aiSystem.Name,
                    Type = aiSystem.Type,
                    Version = aiSystem.Version,
                    Integration = aiSystem.Integration,
                    Purpose = aiSystem.Purpose,
                    DateAdded = aiSystem.DateAdded
                });
        });
        return result;
    }
}