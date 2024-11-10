using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProductCatalogAPI.DTOs.Requests;
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

        if (categories.Count == 0)
        {
            return NoContent();
        }

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory([FromRoute] string id)
    {
        var category = await _categoriesCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto request)
    {
        var existingCategory = await _categoriesCollection
            .Find(c => c.Name == request.Name)
            .FirstOrDefaultAsync();

        if (existingCategory != null)
        {
            return Conflict("A category with the same name already exists.");
        }

        var newCategory = new Category(request.Name, request.Description);

        await _categoriesCollection.InsertOneAsync(newCategory);
        return Ok(newCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] string id, [FromBody] CategoryUpdateDto request)
    {
        var existingCategory = await _categoriesCollection
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (existingCategory == null)
        {
            return NotFound("Category not found.");
        }

        existingCategory.Name = request.Name;
        existingCategory.Description = request.Description;
        existingCategory.IsActive = request.IsActive;
        existingCategory.UpdatedAt = DateTime.UtcNow; 


        var updateResult = await _categoriesCollection.ReplaceOneAsync(
            c => c.Id == id, 
            existingCategory 
        );

        return Ok(existingCategory);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] string id)
    {
        var result = await _categoriesCollection.DeleteOneAsync(c => c.Id == id);
        if (result.DeletedCount == 0)
        {
            return NotFound();
        }
        return NoContent();
    }


}
