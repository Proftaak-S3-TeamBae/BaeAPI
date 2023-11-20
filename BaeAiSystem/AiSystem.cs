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
    /// The description of how the AI system is used.
    /// </summary>
    public string Purpose { get; set; }

    /// <summary>
    /// The date the AI system was registered.
    /// </summary>
    public DateTime DateAdded { get; set; }

    /// <summary>
    /// Generate a unique ID for the AI system by using the name, type, source and version and encoding it in base64.
    /// </summary>
    /// <returns></returns>
    public readonly string GenerateId()
    {
        var json = JsonSerializer.Serialize(new
        {
            Name,
            Type,
            Integration,
            Version
        });
        var bytes = Encoding.UTF8.GetBytes(json);
        return Convert.ToBase64String(bytes);
    }
}
