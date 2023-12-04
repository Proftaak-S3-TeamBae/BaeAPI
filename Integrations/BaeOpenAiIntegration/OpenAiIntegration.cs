using System.Text.Json;
using BaeDB;
using BaeIntegrations;
using BaeOpenAiIntegration.DTO;

namespace BaeOpenAiIntegration;

/// <summary>
/// The service for fetching AI systems.
/// </summary>
public class OpenAiIntegration : IAiServiceIntegration
{
    /// <summary>
    /// The API key for OpenAI.
    /// </summary>
    public string? ApiKey { get; private set; }

    public string Id { get; set; } = "BaeOpenAiIntegration";


    /// <summary>
    /// Register the API key for OpenAI.
    /// </summary>
    /// <param name="key"></param>
    public void RegisterKey(string key)
        => ApiKey = key;

    /// <summary>
    /// Remove the API key for OpenAI from memory.
    /// </summary>
    public void RemoveKey()
        => ApiKey = null;

    public async Task<List<FetchedAiSystem>> GetAiSystemsAsync()
    {
        if (ApiKey == null)
            throw new Exception("No API key provided");

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
        var response = await client.GetAsync("https://api.openai.com/v1/models");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to get engines from OpenAI");
        }

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<OpenAiResultDTO>(content);
        var services = new List<FetchedAiSystem>();

        foreach (var model in result.Data!)
        {
            services.Add(new FetchedAiSystem
            {
                Name = model.Id ?? "Unknown",
                Type = "LLM",
                Integration = 1,
                Purpose = "",
                Version = model.Id ?? "In Name",
                DateAdded = DateTime.Now
            });
        }

        return services;
    }
}
