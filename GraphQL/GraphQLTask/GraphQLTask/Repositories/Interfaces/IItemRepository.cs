using GraphQLTask.Entities;

namespace GraphQLTask.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetByIdAsync(int id);
        IQueryable<Item> GetAll();
        Task<Item> AddAsync(Item item);
        Task<Item> UpdateAsync(Item item);
        Task DeleteAsync(int id);
    }
}
