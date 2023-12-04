
using BaeDB;
using BaeIntegrations;
using Microsoft.EntityFrameworkCore;

namespace BaeOpenAiIntegration.Service;

/// <summary>
/// The service for handling the OpenAI integration.
/// </summary>
public class OpenAiService : IOpenAiService
{
    public AiServiceIntegrationManager _aiServiceIntegrationManager;

    public OpenAiService(AiServiceIntegrationManager aiServiceIntegrationManager)
    {
        _aiServiceIntegrationManager = aiServiceIntegrationManager;
    }

    public void RegisterKey(string key)
    {
        var integration = _aiServiceIntegrationManager.GetIntegration<OpenAiIntegration>();
        integration.RegisterKey(key);
    }

    public void RemoveKey()
    {
        var integration = _aiServiceIntegrationManager.GetIntegration<OpenAiIntegration>();
        integration.RemoveKey();
    }
}
