using GraphQLTask.Entities;
using GraphQLTask.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTask.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class CategoryQuery
    {
        public async Task<List<Category>> GetCategoryListAsync([Service] ICategoryRepository categoryRepository)
        {
            return await categoryRepository.GetAllCategories().ToListAsync();
        }
    }

    public class CategoryType : ObjectType<Category> { }
}
