using GraphQLTask.Entities;
using GraphQLTask.Models;
using GraphQLTask.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTask.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class ItemQuery
    {
        private const int PAGE_SIZE = 10;

        public async Task<List<ItemResponseModel>> GetItemListAsync([Service] IItemRepository itemRepository, [Service] ICategoryRepository categoryRepository, int? categoryId, int pageNumber = 1)
        {
            //NOTE: to save time logic will be implemented here, in real project it should be in services
            IQueryable<Item> query = itemRepository.GetAll();

            if (categoryId is not null)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            List<Item> items = await query.Skip((pageNumber - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToListAsync();

            if (!items.Any())
            {
                return new List<ItemResponseModel>();
            }

            IEnumerable<int> relatedCategoryIds = items.Select(x => x.CategoryId).Distinct();
            List<Category> relatedCategories = await categoryRepository.GetAllCategories().Where(x => relatedCategoryIds.Any(y => x.Id == y)).ToListAsync();


            return items.Select(x => new ItemResponseModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = relatedCategories.First(y => y.Id == x.CategoryId)
            }).ToList();
        }
    }

    public class ItemType : ObjectType<Item> { }
}
