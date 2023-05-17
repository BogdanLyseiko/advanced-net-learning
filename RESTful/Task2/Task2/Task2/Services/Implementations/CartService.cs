using Microsoft.EntityFrameworkCore;
using Task2.DAL.Entities;
using Task2.DAL.Repositories.Interfaces;
using Task2.Services.Interfaces;
using Task2.ViewModels;

namespace Task2.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IItemRepository _itemRepository;

        public CartService(ICartRepository cartRepository, IItemRepository itemRepository)
        {
            _cartRepository = cartRepository;
            _itemRepository = itemRepository;
        }

        public async Task AddItemAsync(string cartId, CreateItemViewModel item)
        {
            Cart? existingCart = await _cartRepository.GetAsync(cartId);

            if (existingCart is null)
            {
                await _cartRepository.AddAsync(new Cart { Id = cartId });
            }

            await _itemRepository.AddAsync(new Item
            {
                Name = item.Name,
                CartId = cartId
            });
        }

        public async Task<bool> DeleteItemAsync(string cartId, int itemId)
        {
            Item? existingItem = await _itemRepository.Get().Where(x => x.Id == itemId && x.CartId == cartId).FirstOrDefaultAsync();

            if (existingItem is null)
            {
                return false;
            }

            existingItem.CartId = null;

            await _itemRepository.UpdateAsync(existingItem);

            return true;
        }

        public async Task<CartResponseViewModel?> GetAsync(string id)
        {
            Cart? cart = await _cartRepository.GetAsync(id);

            if (cart is null)
            {
                return null;
            }

            List<ItemResponseViewModel> items = await _itemRepository.Get().Where(x => x.CartId == id).Select(x => new ItemResponseViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new CartResponseViewModel
            {
                Id = cart.Id,
                Items = items
            };
        }

        public async Task<List<ItemResponseViewModel>> GetItems(string cartId)
        {
            return await _itemRepository.Get()
                .Where(x => x.CartId == cartId)
                .Select(x => new ItemResponseViewModel { Id = x.Id, Name = x.Name })
                .ToListAsync();
        }
    }
}
