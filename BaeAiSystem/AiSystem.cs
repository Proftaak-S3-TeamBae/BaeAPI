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
    public string Source { get; set; }

    /// <summary>
    /// The description of how the AI system is used.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The date the AI system was registered.
    /// </summary>
    public DateTime DateAdded { get; set; }
}
