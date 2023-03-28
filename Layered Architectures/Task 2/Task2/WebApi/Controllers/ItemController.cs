using Application.Services.Interfaces;
using Application.ViewModels;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<List<Item>> GetAsync()
        {
            return await _itemService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<Item?> GetAsync(int id)
        {
            return await _itemService.GetAsync(id);
        }

        [HttpPost]
        public async Task<Item?> Post(ItemRequestViewModel item)
        {
            return await _itemService.CreateAsync(item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Item?>> Put(int id, [FromBody] ItemRequestViewModel item)
        {
            try
            {
                return Ok(await _itemService.UpdateAsync(id, item));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _itemService.DeleteAsync(id);
        }
    }
}
