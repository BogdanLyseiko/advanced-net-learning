using GraphQLTask.Entities;
using GraphQLTask.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTask.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class CategoryMutation
    {
        public async Task<bool> AddCategoryAsync([Service] ICategoryRepository categoryRepository, Category category)
        {
            await categoryRepository.CreateCategoryAsync(category);

            return true;
        }

        public async Task<bool> UpdateCategoryAsync([Service] ICategoryRepository categoryRepository, Category category)
        {
            Category? existing = await categoryRepository.GetCategoryByIdAsync(category.Id);

            if (existing is null)
            {
                throw new GraphQLException("Category does not exist");
            }

            existing.Name = category.Name;

            await categoryRepository.UpdateCategoryAsync(category);

            return true;
        }

        public async Task<bool> DeleteCategoryAsync([Service] ICategoryRepository categoryRepository, [Service] IItemRepository itemRepository, int categoryId)
        {
            Category? existing = await categoryRepository.GetCategoryByIdAsync(categoryId);

            if (existing is null)
            {
                throw new GraphQLException("Category does not exist");
            }

            List<int> itemsToDelete = await itemRepository.GetAll().Where(x => x.CategoryId == categoryId).Select(x => x.Id).ToListAsync();

            if (itemsToDelete.Any())
            {
                foreach (int item in itemsToDelete)
                {
                    await itemRepository.DeleteAsync(item);
                }
            }

            await categoryRepository.DeleteCategoryAsync(categoryId);

            return true;
        }
    }
}
