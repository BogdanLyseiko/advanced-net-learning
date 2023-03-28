using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        IQueryable<TEntity> Get();
        Task<TEntity?> GetAsync(int id);
        Task<TEntity?> CreateAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
