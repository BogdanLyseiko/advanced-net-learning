using Microsoft.AspNetCore.Mvc;
using Task2.Services.Interfaces;
using Task2.ViewModels;

namespace Task2.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Get Cart model with related items by cart id
        /// </summary>
        /// <param name="id">CartId</param>
        /// <returns>Cart model</returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public async Task<CartResponseViewModel?> Get(string id)
        {
            return await _cartService.GetAsync(id);
        }

        /// <summary>
        /// Add item to cart
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <param name="item">Item model</param>
        [MapToApiVersion("1.0")]
        [HttpPost("{id}/item")]
        public async Task AddItem(string id, [FromBody] CreateItemViewModel item)
        {
            await _cartService.AddItemAsync(id, item);
        }

        /// <summary>
        /// Remove item from cart
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <param name="itemId">Item id</param>
        [MapToApiVersion("1.0")]
        [HttpDelete("{id}/item/{itemId}")]
        public async Task<ActionResult> Delete(string id, int itemId)
        {
            bool deleted = await _cartService.DeleteItemAsync(id, itemId);

            if (deleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
