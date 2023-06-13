using GraphQLTask.Entities;
using GraphQLTask.Repositories.Interfaces;

namespace GraphQLTask.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class ItemMutation
    {
        public async Task<bool> AddItemAsync([Service] IItemRepository itemRepository, Item item)
        {
            await itemRepository.AddAsync(item);

            return true;
        }

        public async Task<bool> UpdateItemAsync([Service] IItemRepository itemRepository, Item item)
        {
            Item? existing = await itemRepository.GetByIdAsync(item.Id);

            if (existing is null)
            {
                throw new GraphQLException("Item does not exist");
            }

            existing.Name = item.Name;
            existing.CategoryId = item.CategoryId;

            await itemRepository.UpdateAsync(item);

            return true;
        }

        public async Task<bool> DeleteItemAsync([Service] IItemRepository itemRepository, int itemId)
        {
            Item? existing = await itemRepository.GetByIdAsync(itemId);

            if (existing is null)
            {
                throw new GraphQLException("Item does not exist");
            }

            await itemRepository.DeleteAsync(itemId);

            return true;
        }
    }
}
