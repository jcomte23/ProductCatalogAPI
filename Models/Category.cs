using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductCatalogAPI.Models;
public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("description")]
    public string Description { get; set; } 

    [BsonElement("isActive")]
    [BsonDefaultValue(true)]
    public bool IsActive { get; set; } 

    [BsonElement("createdAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; set; } 

    [BsonElement("updatedAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime UpdatedAt { get; set; }

    public Category(string name, string description)
    {
        Name = name;
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Category(string name, string description, bool isActive, DateTime updatedAt)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        UpdatedAt = updatedAt;
    }
}
