using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IMongoCollection<Category> _categoriesCollection;

    public CategoriesController(IMongoCollection<Category> categoriesCollection)
    {
        _categoriesCollection = categoriesCollection;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _categoriesCollection.Find(_ => true).ToListAsync();
        return Ok(categories);
    }

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

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        await _categoriesCollection.InsertOneAsync(category);
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(string id, Category category)
    {
        var result = await _categoriesCollection.ReplaceOneAsync(c => c.Id == id, category);
        if (result.MatchedCount == 0)
        {
            return NotFound();
        }
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        var result = await _categoriesCollection.DeleteOneAsync(c => c.Id == id);
        if (result.DeletedCount == 0)
        {
            return NotFound();
        }
        return NoContent();
    }


}
