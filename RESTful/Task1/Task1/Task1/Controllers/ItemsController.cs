using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1.DAL.Entities;
using Task1.DAL.Repositories.Interfaces;
using Task1.ViewModels;

namespace Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // GET: api/<ItemsController>
        [HttpGet]
        public async Task<IEnumerable<ItemResponseModel>> Get(int? categoryId, int? pageNumber)
        {
            // To simplify code, added default but we can pass it from client
            int defaultPageSize = 2;

            IQueryable<Item> items = _itemRepository.Get();

            if (categoryId is not null)
            {
                items = items.Where(x => x.CategoryId == categoryId);
            }

            if (pageNumber is not null)
            {
                items = items.Skip((pageNumber.Value - 1) * defaultPageSize).Take(defaultPageSize);
            }

            return (await items.ToListAsync()).Select(x => new ItemResponseModel
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId
            }).ToList();
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public async Task<Item?> Get(int id)
        {
            return await _itemRepository.GetAsync(id);
        }

        // POST api/<ItemsController>
        [HttpPost]
        public async Task Post([FromBody] Item item)
        {
            await _itemRepository.CreateAsync(item);
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Item item)
        {
            item.Id = id;

            await _itemRepository.UpdateAsync(item);
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _itemRepository.DeleteAsync(id);
        }
    }
}
