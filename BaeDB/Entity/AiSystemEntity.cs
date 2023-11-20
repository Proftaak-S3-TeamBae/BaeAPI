namespace BaeDB.Entity;

/// <summary>
/// The database entity for an AI system.
/// </summary>
public class AiSystemEntity
{
    public AiSystemEntity(string id, string name, string type, string purpose, DateTime dateadded, int intergration, string version)
    {
        Id = id;
        Name = name;
        Type = type;
        Purpose = purpose;
        DateAdded = dateadded;
        Integration = intergration;
        Version = version;
    }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Purpose { get; set; }
    public DateTime DateAdded { get; set; }
    public int Integration { get; set; }
    public string Version { get; set; }
}