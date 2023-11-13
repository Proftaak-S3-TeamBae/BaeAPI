using System.Text.Json.Serialization;

namespace BaeOpenAiIntegration.DTO;

/// <summary>
/// The DTO for the result of a permission check
/// </summary>
public struct OpenAiPermissionResultDTO
{
    /// <summary>
    /// The id of the model
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// What kind of model it is
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// When the model was created
    /// </summary>
    [JsonPropertyName("created")]
    public DateTime? Created { get; set; }

    [JsonPropertyName("allow_create_engine")]
    public bool AllowCreateEngine { get; set; }

    [JsonPropertyName("allow_sampling")]
    public bool AllowSampling { get; set; }

    [JsonPropertyName("allow_log_probs")]
    public bool AllowLogProbs { get; set; }

    [JsonPropertyName("allow_search_indices")]

    public bool AllowSearchIndices { get; set; }

    [JsonPropertyName("allow_view")]
    public bool AllowView { get; set; }

    [JsonPropertyName("allow_fine_tuning")]
    public bool AllowFineTuning { get; set; }

    [JsonPropertyName("is_blocking")]
    public bool IsBlocking { get; set; }
}
