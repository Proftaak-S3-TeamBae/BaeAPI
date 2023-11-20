using MongoDB.Bson.Serialization.Attributes;

namespace BaeDB.Entity;

/// <summary>
/// The link table for disapproved AI systems.
/// </summary>
public class DisapprovedAiSystemLinkTable
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string AiSystemId { get; set; }
}
