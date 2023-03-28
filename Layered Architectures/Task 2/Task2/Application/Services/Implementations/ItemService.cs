using Application.Services.Interfaces;
using Application.ViewModels;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _repository;

        public ItemService(IRepository<Item> repository)
        {
            _repository = repository;
        }

        public async Task<Item?> CreateAsync(ItemRequestViewModel item)
        {
            return await _repository.CreateAsync(new Item
            {
                Name = item.Name,
                Description = item.Description,
                Amount = item.Amount,
                CategoryId = item.CategoryId,
                Image = item.Image,
                Price = item.Price,
            });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<Item>> GetAsync()
        {
            return await _repository.Get().ToListAsync();
        }

        public async Task<Item?> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<Item?> UpdateAsync(int id, ItemRequestViewModel item)
        {
            Item? existing = await _repository.GetAsync(id);

            if (existing == null)
            {
                throw new EntityNotFoundException("Item not found");
            }

            existing.Amount = item.Amount;
            existing.CategoryId = item.CategoryId;
            existing.Image = item.Image;
            existing.Price = item.Price;
            existing.Description = item.Description;
            existing.Name = item.Name;

            return await _repository.UpdateAsync(existing);
        }
    }
}
