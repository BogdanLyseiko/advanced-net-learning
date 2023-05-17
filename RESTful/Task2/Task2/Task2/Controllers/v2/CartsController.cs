using Microsoft.AspNetCore.Mvc;
using Task2.Services.Interfaces;
using Task2.ViewModels;

namespace Task2.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Get items for particular cart
        /// </summary>
        /// <param name="id">cart id</param>
        /// <returns>list of related items</returns>
        [MapToApiVersion("2.0")]
        [HttpGet("{id}")]
        public async Task<List<ItemResponseViewModel>> Get(string id)
        {
            return await _cartService.GetItems(id);
        }
    }
}
