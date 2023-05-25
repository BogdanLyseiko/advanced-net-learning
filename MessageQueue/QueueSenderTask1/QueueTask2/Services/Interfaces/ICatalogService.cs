using QueueTask2.DAL.Entities;

namespace QueueTask2.Services.Interfaces
{
    public interface ICatalogService
    {
        void Update(int id, CatalogItem catalogItem);
    }
}
