using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1.DAL.Entities;
using Task1.DAL.Repositories.Interfaces;


namespace Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;

        public CategoriesController(ICategoryRepository categoryRepository, IItemRepository itemRepository)
        {
            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _categoryRepository.Get().ToListAsync();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<Category?> Get(int id)
        {
            return await _categoryRepository.GetAsync(id);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task Post([FromBody] Category category)
        {
            await _categoryRepository.CreateAsync(category);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Category category)
        {
            category.Id = id;

            await _categoryRepository.UpdateAsync(category);
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            List<Item> itemsToDelete = await _itemRepository.Get().Where(x => x.CategoryId == id).ToListAsync();

            if (itemsToDelete.Any())
            {
                await _itemRepository.DeleteAsync(itemsToDelete);
            }

            await _categoryRepository.DeleteAsync(id);
        }
    }
}
