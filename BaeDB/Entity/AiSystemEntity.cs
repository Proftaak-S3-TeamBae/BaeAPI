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
    public DateTime DateAdded { get; set; }
    public int Integration { get; set; }
    public string Version { get; set; }
}