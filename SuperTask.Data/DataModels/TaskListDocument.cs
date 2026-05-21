using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SuperTask.Data.DataModels;

public sealed class TaskListDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    [BsonRepresentation(BsonType.String)]
    public Guid OwnerUserId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public List<Guid> SharedUserIds { get; set; } = [];

    public DateTime CreatedAt { get; set; }
}