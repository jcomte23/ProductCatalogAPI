using MongoDB.Driver;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categoriesCollection;

        public CategoryRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("ProductCatalogDB");
            _categoriesCollection = database.GetCollection<Category>("Categories");
        }

        public async Task CreateAsync(Category category)
        {
            await _categoriesCollection.InsertOneAsync(category);
        }

        public async Task DeleteAsync(string id)
        {
            await _categoriesCollection.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoriesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _categoriesCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            await _categoriesCollection.ReplaceOneAsync(c => c.Id == category.Id, category);
        }
    }
}
