using Task1.DAL.Entities;

namespace Task1.DAL.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetAsync(int id);
        IQueryable<Item> Get();
        Task CreateAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeleteAsync(int id);
        Task DeleteAsync(IEnumerable<Item> itemsToDelete);
    }
}
