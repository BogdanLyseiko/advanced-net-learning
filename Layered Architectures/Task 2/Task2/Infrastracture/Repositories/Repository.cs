using Domain.Entities;
using Domain.Repositories;
using Infrastracture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastracture.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity?> CreateAsync(TEntity entity)
        {
            EntityEntry<TEntity> created = await _dbContext.Set<TEntity>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return created?.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            TEntity entityToRemove = new() { Id = id };
            DbSet<TEntity> entitySet = _dbContext.Set<TEntity>();

            entitySet.Attach(entityToRemove);
            entitySet.Remove(entityToRemove);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public IQueryable<TEntity> Get()
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            EntityEntry<TEntity> updated = _dbContext.Set<TEntity>().Update(entity);

            await _dbContext.SaveChangesAsync();

            return updated?.Entity;
        }
    }
}
