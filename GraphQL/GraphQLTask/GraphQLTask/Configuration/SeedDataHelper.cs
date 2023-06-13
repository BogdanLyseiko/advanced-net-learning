using GraphQLTask.Entities;

namespace GraphQLTask.Configuration
{
    public static class SeedDataHelper
    {
        public static void InitializeInMemoryDatabase(WebApplication app)
        {
            IServiceScope scope = app.Services.CreateScope();
            AppDbContext? db = scope.ServiceProvider.GetService<AppDbContext>();

            if (db is not null)
            {
                db.Categories.AddRange(new List<Category>
                {
                    new Category { Id = 1, Name = "Category1" },
                    new Category { Id = 2, Name = "Category2" },
                    new Category { Id = 3, Name = "Category3" }
                });

                db.Items.AddRange(new List<Item>
                {
                    new Item { Id = 1, Name = "Item1WithCategory1", CategoryId = 1 },
                    new Item { Id = 2, Name = "Item2WithCategory1", CategoryId = 1 },
                    new Item { Id = 3, Name = "Item3WithCategory2", CategoryId = 2 },
                });

                db.SaveChanges();
            }
        }
    }
}
