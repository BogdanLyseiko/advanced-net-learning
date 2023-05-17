using Task2.ViewModels;

namespace Task2.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartResponseViewModel?> GetAsync(string id);
        Task AddItemAsync(string cartId, CreateItemViewModel item);
        Task<bool> DeleteItemAsync(string cartId, int itemId);
        Task<List<ItemResponseViewModel>> GetItems(string cartId);
    }
}
