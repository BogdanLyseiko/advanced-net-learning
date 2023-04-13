using Microsoft.EntityFrameworkCore;
using Task1.DAL.Entities;
using Task1.DAL.Repositories.Interfaces;

namespace Task1.DAL.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Item item)
        {
            _context.Items.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task<Item?> GetAsync(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Item> Get()
        {
            return _context.Items;
        }

        public async Task UpdateAsync(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Item? existing = await GetAsync(id);

            if (existing != null)
            {
                _context.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(IEnumerable<Item> itemsToDelete)
        {
            if (itemsToDelete.Any())
            {
                _context.RemoveRange(itemsToDelete);

                await _context.SaveChangesAsync();
            }
        }
    }
}
