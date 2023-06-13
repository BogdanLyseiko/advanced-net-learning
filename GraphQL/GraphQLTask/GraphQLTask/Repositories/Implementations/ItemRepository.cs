using GraphQLTask.Entities;
using GraphQLTask.Repositories.Interfaces;

namespace GraphQLTask.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _dbContext;

        public ItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _dbContext.Items.FindAsync(id);
        }

        public IQueryable<Item> GetAll()
        {
            return _dbContext.Items;
        }

        public async Task<Item> AddAsync(Item item)
        {
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item> UpdateAsync(Item item)
        {
            _dbContext.Items.Update(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            Item? item = await _dbContext.Items.FindAsync(id);
            if (item != null)
            {
                _dbContext.Items.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
