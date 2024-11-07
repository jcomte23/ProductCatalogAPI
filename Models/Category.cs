using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductCatalogAPI.Models;
public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    [Required(ErrorMessage = "The Name field is required.")]
    [MinLength(3, ErrorMessage = "The Name must be at least 3 characters long.")]
    [MaxLength(50, ErrorMessage = "The Name must not exceed 50 characters.")]
    public required string Name { get; set; }

    [BsonElement("description")]
    [Required(ErrorMessage = "The Description field is required.")]
    [MinLength(10, ErrorMessage = "The Description must be at least 10 characters long.")]
    [MaxLength(250, ErrorMessage = "The Description must not exceed 250 characters.")]
    public required string Description { get; set; }

    [BsonElement("isActive")]
    [BsonDefaultValue(true)]
    public bool IsActive { get; set; } = true;

    [BsonElement("createdAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
