using System.Text.Json.Serialization;

namespace BaeOpenAiIntegration.DTO;

/// <summary>
/// The result of a request to OpenAI for a model
/// </summary>
public struct OpenAiModelResultDTO
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
    public int? Created { get; set; }

    /// <summary>
    /// The owner of the model
    /// </summary>
    [JsonPropertyName("owned_by")]
    public string? OwnedBy { get; set; }

    /// <summary>
    /// The permissions of the model
    /// </summary>
    [JsonPropertyName("permissions")]
    public OpenAiPermissionResultDTO? Permissions { get; set; }

    /// <summary>
    /// The root of the object
    /// </summary>
    [JsonPropertyName("root")]
    public string? Root { get; set; }

    /// <summary>
    /// The parent of the object
    /// </summary>
    [JsonPropertyName("parent")]
    public string? Parent { get; set; }
}