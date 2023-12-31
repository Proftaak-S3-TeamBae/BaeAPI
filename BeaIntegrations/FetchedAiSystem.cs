using System.Text.Json.Serialization;

namespace BaeIntegrations;

/// <summary>
/// An AI system fetched from an integration.
/// </summary>
[Serializable]
public struct FetchedAiSystem
{
    /// <summary>
    /// The name of the AI system.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// The type of the AI system.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// The service the AI system is hosted on.
    /// </summary>
    [JsonPropertyName("source")]
    public int Integration { get; set; }

    /// <summary>
    /// The identifier of the origin account
    /// </summary>
    [JsonPropertyName("origin")]
    public string Origin;

    /// <summary>
    /// The version of the AI system.
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; }

    /// <summary>
    /// The description of how the AI system is used.
    /// </summary>
    [JsonPropertyName("purpose")]
    public string Purpose { get; set; }

    /// <summary>
    /// The date the AI system was registered.
    /// </summary>
    [JsonPropertyName("date_added")]
    public DateTime DateAdded { get; set; }
}
