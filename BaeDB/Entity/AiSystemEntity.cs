namespace BaeDB.Entity;

/// <summary>
/// The database entity for an AI system.
/// </summary>
public class AiSystemEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Purpose { get; set; }
    public string Description { get; set; }
    public DateTime DateAdded { get; set; }
    public int Integration { get; set; }
    public string Origin { get; set; }
    public string Version { get; set; }
}