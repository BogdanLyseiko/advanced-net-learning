using Task1.DAL.Entities;

namespace Task1.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetAsync(int id);
        IQueryable<Category> Get();
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
