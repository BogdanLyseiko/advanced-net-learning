using GraphQLTask.Entities;

namespace GraphQLTask.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAllCategories();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
