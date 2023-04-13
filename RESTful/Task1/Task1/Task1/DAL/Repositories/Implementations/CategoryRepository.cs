using Microsoft.EntityFrameworkCore;
using Task1.DAL.Entities;
using Task1.DAL.Repositories.Interfaces;

namespace Task1.DAL.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();
        }

        public async Task<Category?> GetAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Category> Get()
        {
            return _context.Categories;
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category? existing = await GetAsync(id);

            if (existing != null)
            {
                _context.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
