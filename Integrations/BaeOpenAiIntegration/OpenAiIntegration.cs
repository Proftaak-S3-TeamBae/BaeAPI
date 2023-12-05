using System.Security;
using System.Text.Json;
using BaeDB;
using BaeIntegrations;
using BaeIntegrations.Exceptions;
using BaeOpenAiIntegration.DTO;

namespace BaeOpenAiIntegration;

/// <summary>
/// The service for fetching AI systems.
/// </summary>
public class OpenAiIntegration : IAiServiceIntegration
{
    /// <summary>
    /// The API keys for OpenAI.
    /// </summary>
    public List<string> ApiKeys { get; private set; } = new();

    public string Id { get; set; } = "BaeOpenAiIntegration";


    /// <summary>
    /// Register the API key for OpenAI.
    /// </summary>
    /// <param name="key"></param>
    public void RegisterKey(string key)
        => ApiKeys.Add(key);

    /// <summary>
    /// Remove the API key for OpenAI from memory.
    /// </summary>
    public void RemoveKeys()
        => ApiKeys.Clear();

    public async Task<List<FetchedAiSystem>> GetAiSystemsAsync()
    {
        var result = new List<FetchedAiSystem>();
        foreach (var key in ApiKeys)
            result.AddRange(await GetAiSystemsFromKeyAsync(key));
        return result;
    }

    /// <summary>
    /// Get the ai systems registered to one key
    /// </summary>
    /// <param name="key"></param>
    private static async Task<List<FetchedAiSystem>> GetAiSystemsFromKeyAsync(string key)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");
        var response = await client.GetAsync("https://api.openai.com/v1/models");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to get engines from OpenAI: " + response.StatusCode + " - " + response.ReasonPhrase + " - " + await response.Content.ReadAsStringAsync());
        }

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<OpenAiResultDTO>(content);
        var services = new List<FetchedAiSystem>();

        foreach (var model in result.Data!)
        {
            services.Add(new FetchedAiSystem
            {
                Name = model.Id ?? "Unknown",
                Type = DetectModelType(model.Id ?? "Unknown"),
                Integration = 1,
                Purpose = "",
                Origin = CensorApiKey(key),
                Version = model.Id ?? "In Name",
                DateAdded = DateTime.Now
            });
        }

        return services;
    }

    /// <summary>
    /// Censors the api key so it can be used as an identifier
    /// </summary>
    /// <returns>The censored api key</returns>
    private static string CensorApiKey(string key)
        => $"{key[..3]}...{key[^4..]}";

    /// <summary>
    /// Attempt to auto-detect the model type from the model id.
    /// This is not guaranteed to be correct, and should be changeable by the user.
    /// </summary>
    /// <param name="modelId"></param>
    /// <returns></returns>
    private static string DetectModelType(string modelId)
    {
        if (modelId.Contains("gpt"))
            return "LLM";
        if (modelId.Contains("davinci"))
            return "LLM";
        if (modelId.Contains("curie"))
            return "LLM";
        if (modelId.Contains("babbage"))
            return "LLM";
        if (modelId.Contains("ada"))
            return "LLM";
        if (modelId.Contains("tts"))
            return "TTS";
        if (modelId.Contains("dall-e"))
            return "Image";

        return "Unknown";
    }
}
