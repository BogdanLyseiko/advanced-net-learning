using Task2.DAL.Entities;

namespace Task2.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetAsync(string id);
        Task AddAsync(Cart cart);
    }
}
