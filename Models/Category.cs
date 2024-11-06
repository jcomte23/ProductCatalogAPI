namespace ProductCatalogAPI.Models;
public class Category
{
    public string? Id { get; set; }  // MongoDB genera un Id único por defecto
    public string Name { get; set; }
    public string Description { get; set; }

    public Category(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
