using Microsoft.EntityFrameworkCore;
using Task1.DAL.Entities;

namespace Task1.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
