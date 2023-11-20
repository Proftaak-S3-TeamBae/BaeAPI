using System.Data.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace BaeDB.Entity;

/// <summary>
/// The link table for approved AI systems.
/// </summary>
public class ApprovedAiSystemLinkTable
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string AiSystemId { get; set; }
}
