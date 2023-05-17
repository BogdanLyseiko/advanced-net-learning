using Task2.DAL.Entities;

namespace Task2.DAL.Repositories.Interfaces
{
    public interface IItemRepository
    {
        IQueryable<Item> Get();

        Task AddAsync(Item item);

        Task UpdateAsync(Item item);
    }
}
