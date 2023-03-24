using Microsoft.AspNetCore.Mvc;
using Task1Project.BLL.Interfaces;
using Task1Project.BLL.ViewModels;

namespace Task1Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public List<CartViewModel> Get()
        {
            return _cartService.GetAll();
        }

        [HttpPost]
        public int Post(CartViewModel entity)
        {
            return _cartService.Create(entity);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return _cartService.Delete(id);
        }
    }
}
