namespace BaeOpenAiIntegration.DTO;

using System.Text.Json.Serialization;

/// <summary>
/// The result of a request to OpenAI
/// </summary>
public struct OpenAiResultDTO
{
    /// <summary>
    /// The object kind
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// The data in the object
    /// </summary>
    [JsonPropertyName("data")]
    public List<OpenAiModelResultDTO>? Data { get; set; }
}
