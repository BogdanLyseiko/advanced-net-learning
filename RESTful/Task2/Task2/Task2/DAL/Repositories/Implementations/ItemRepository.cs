using Task2.DAL.Entities;
using Task2.DAL.Repositories.Interfaces;

namespace Task2.DAL.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Item item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Item> Get()
        {
            return _context.Items;
        }

        public async Task UpdateAsync(Item item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
