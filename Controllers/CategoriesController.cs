using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IMongoCollection<Category> _categoriesCollection;

    // El constructor ahora recibe la colección inyectada
    public CategoriesController(IMongoCollection<Category> categoriesCollection)
    {
        _categoriesCollection = categoriesCollection;
    }

    // GET: api/categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _categoriesCollection.Find(_ => true).ToListAsync();
        return Ok(categories);
    }

        // GET: api/categories/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(string id)
    {
        var category = await _categoriesCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }
}
