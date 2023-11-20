using MongoDB.Bson.Serialization.Attributes;

namespace BaeDB.Entity;

/// <summary>
/// The database entity for the openai integration.
/// </summary>
public class OpenAiIntegrationEntity
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string ApiKey { get; set; }
}
