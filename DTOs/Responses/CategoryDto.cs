namespace ProductCatalogAPI.DTOs.Responses;
public class CategoryDto(string id, string name, string descripcion)
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = descripcion;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

