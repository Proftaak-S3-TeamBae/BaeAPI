namespace BaeOpenAiIntegration.Service;

/// <summary>
/// The service for handling the OpenAI integration.
/// </summary>
public interface IOpenAiService
{
    /// <summary>
    /// Register an OpenAI API key.
    /// This will get stored in the database.
    /// </summary>
    /// <returns></returns>
    public Task RegisterKey(string key);

    /// <summary>
    /// Remove an OpenAI API key.
    /// This will remove the key from the database.
    /// </summary>
    /// <returns></returns>
    public Task RemoveKey();
}
