using LiteDB;
using Task1Project.DAL.Configuration;
using Task1Project.DAL.Entities;
using Task1Project.DAL.Repositories.Interfaces;

namespace Task1Project.DAL.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly LiteDatabase _liteDb;

        public Repository(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public int Create(TEntity entity)
        {
            return _liteDb.GetCollection<TEntity>().Insert(entity);
        }

        public bool Delete(int id)
        {
            return _liteDb.GetCollection<TEntity>().Delete(id);
        }

        public List<TEntity> Get()
        {
            return _liteDb.GetCollection<TEntity>().FindAll().ToList();
        }

        public TEntity? Get(int id)
        {
            return _liteDb.GetCollection<TEntity>().FindOne(x => x.Id == id);
        }
    }
}
