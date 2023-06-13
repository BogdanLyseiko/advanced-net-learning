using GraphQLTask.Entities;
using GraphQLTask.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTask.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Category> GetAllCategories()
        {
            return _dbContext.Categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task CreateCategoryAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            Category? category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
