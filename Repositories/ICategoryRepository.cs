using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Repositories;
public interface ICategoryRepository
{
    Task<Category> GetByIdAsync(string id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task CreateAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(string id);
}

