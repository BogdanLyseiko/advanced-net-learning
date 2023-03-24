using Task1Project.DAL.Entities;

namespace Task1Project.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        List<TEntity> Get();

        TEntity? Get(int id);

        int Create(TEntity entity);

        bool Delete(int id);
    }
}
