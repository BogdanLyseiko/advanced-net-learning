using QueueTask2.DAL.Entities;
using QueueTask2.Services.Interfaces;

namespace QueueTask2.Services.Implementations
{
    public class CatalogService : ICatalogService
    {
        private readonly ServiceBusSender _serviceBusSender;

        public CatalogService(ServiceBusSender serviceBusSender)
        {
            _serviceBusSender = serviceBusSender;
        }



        // To simplify project decided to use static list instead of db 
        private static readonly List<CatalogItem> _catalogItems = new()
        {
            new CatalogItem() { Id = 1, Name = "CItem1", Price = 2.3M },
            new CatalogItem() { Id = 2, Name = "CItem2", Price = 5M },
            new CatalogItem() { Id = 3, Name = "CItem3", Price = 20M },
            new CatalogItem() { Id = 4, Name = "CItem4", Price = 1M }
        };

        public async void Update(int id, CatalogItem catalogItem)
        {
            CatalogItem? existing = _catalogItems.FirstOrDefault(x => x.Id == id);

            if (existing is not null)
            {
                existing.Price = catalogItem.Price;
                existing.Name = catalogItem.Name;

                await _serviceBusSender.SendMessage(existing);
            }
        }
    }
}
