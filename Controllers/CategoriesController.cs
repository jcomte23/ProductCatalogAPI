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
    public async Task<IActionResult> CreateCategory([FromBody] Category newCategory)
    {
        var existingCategory = await _categoriesCollection
            .Find(c => c.Name == newCategory.Name)
            .FirstOrDefaultAsync();

        if (existingCategory != null)
        {
            return Conflict("A category with the same name already exists.");
        }

        await _categoriesCollection.InsertOneAsync(newCategory);
        return CreatedAtAction(nameof(GetCategory), new { id = newCategory.Id }, newCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(string id, [FromBody] Category updatedCategory)
    {
        var existingCategory = await _categoriesCollection
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (existingCategory == null)
        {
            return NotFound("Category not found.");
        }

        // Actualizar el campo updatedAt
        updatedCategory.UpdatedAt = DateTime.UtcNow;

        // Realizar la actualización
        var updateResult = await _categoriesCollection.ReplaceOneAsync(
            c => c.Id == id, updatedCategory);

        if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
        {
            return NoContent();
        }

        return BadRequest("Update failed.");
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
