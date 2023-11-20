
using BaeDB;
using Microsoft.EntityFrameworkCore;

namespace BaeOpenAiIntegration.Service;

/// <summary>
/// The service for handling the OpenAI integration.
/// </summary>
public class OpenAiService : IOpenAiService
{
    private readonly BaeDbContext _dbContext;

    public OpenAiService(BaeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RegisterKeyAsync(string key)
    {
        // Remove the old key if it exists
        var oldEntity = await _dbContext.OpenAiIntegration.FirstOrDefaultAsync();
        if (oldEntity != null)
        {
            _dbContext.Remove(oldEntity);
        }

        // Add the new key
        _dbContext.Add(new BaeDB.Entity.OpenAiIntegrationEntity
        {
            Id = Guid.NewGuid().ToString(),
            ApiKey = key
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveKeyAsync()
    {
        var entity = await _dbContext.OpenAiIntegration.FirstOrDefaultAsync();
        if (entity != null)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
