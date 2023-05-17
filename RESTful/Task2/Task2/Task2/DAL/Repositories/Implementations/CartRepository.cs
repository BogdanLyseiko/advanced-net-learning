using Microsoft.EntityFrameworkCore;
using Task2.DAL.Entities;
using Task2.DAL.Repositories.Interfaces;

namespace Task2.DAL.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart?> GetAsync(string id)
        {
            return await _context.Carts.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
