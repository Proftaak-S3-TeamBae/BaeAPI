using System.Text.Json;
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
    public string ApiKey { get; set; }

    public string Id { get; set; } = "BaeOpenAiIntegration";

    public OpenAiIntegration(string apiKey)
    {
        ApiKey = apiKey;
    }

    public async Task<List<FetchedAiSystem>> GetAiSystemsAsync()
    {
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
                Source = "OpenAI",
                Description = "",
                DateAdded = DateTime.Now
            });
        }

        return services;
    }
}
