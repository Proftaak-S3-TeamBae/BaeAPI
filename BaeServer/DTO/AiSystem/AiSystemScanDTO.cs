using System.Text.Json.Serialization;

namespace BaeServer.DTO.AiSystem;

/// <summary>
/// The DTO for an ai scan request
/// </summary>
[Serializable]
public class AiSystemScanDTO
{
    /// <summary>
    /// The token for the OpenAI API integration
    /// </summary>
    [JsonPropertyName("openai_tokens")]
    public List<string>? OpenAiTokens { get; set; }

    /// <summary>
    /// The page of the results to return
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; set; }

    /// <summary>
    /// The maximum number of results to return
    /// </summary>
    [JsonPropertyName("max")]
    public int Max { get; set; }
}
