using System.Text;
using System.Text.Json;

namespace BaeAiSystem;

/// <summary>
/// The data of an AI system
/// </summary>
public struct AiSystem
{
    /// <summary>
    /// The name of the AI system.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The type of the AI system.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// The service the AI system is hosted on.
    /// </summary>
    public int Integration { get; set; }

    /// <summary>
    /// The version of the AI system.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// The identifier of the origin account 
    /// </summary>
    public string Origin { get; set; }

    /// <summary>
    /// The description of the AI system.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The description of how the AI system is used.
    /// </summary>
    public string Purpose { get; set; }

    /// <summary>
    /// The date the AI system was registered.
    /// </summary>
    public DateTime DateAdded { get; set; }

    /// <summary>
    /// Generate a unique ID for the AI system by using the name, source, origin and version and encoding it in base64.
    /// </summary>
    /// <returns></returns>
    public readonly string GenerateId()
        => Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new
        {
            Name,
            Integration,
            Origin,
            Version
        })));
}
