using Application.ViewModels;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IItemService
    {
        public Task<List<Item>> GetAsync();
        public Task<Item?> GetAsync(int id);
        public Task<Item?> CreateAsync(ItemRequestViewModel item);
        public Task<Item?> UpdateAsync(int id, ItemRequestViewModel item);
        public Task<bool> DeleteAsync(int id);
    }
}
