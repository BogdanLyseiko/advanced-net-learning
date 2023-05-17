using Microsoft.EntityFrameworkCore;
using Task2.DAL.Entities;

namespace Task2.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
