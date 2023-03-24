using Task1Project.BLL.Interfaces;
using Task1Project.BLL.ViewModels;
using Task1Project.DAL.Entities;
using Task1Project.DAL.Repositories.Interfaces;

namespace Task1Project.BLL.Implementations
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;

        public CartService(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public int Create(CartViewModel entity)
        {
            return _cartRepository.Create(ConvertCartViewModelToCart(entity));
        }

        public bool Delete(int id)
        {
            return _cartRepository.Delete(id);
        }

        public List<CartViewModel> GetAll()
        {
            return _cartRepository.Get().Select(ConvertCartToCartViewModel).ToList();
        }

        //We can add automapper that will do it for us but for test task it will be faster than configure automapper
        private CartViewModel ConvertCartToCartViewModel(Cart cart)
        {
            return new CartViewModel
            {
                Id = cart.Id,
                Name = cart.Name,
                Price = cart.Price,
                Quantity = cart.Quantity,
                Image = cart.Image is not null ? new ImageViewModel
                {
                    Url = cart.Image.Url,
                    AltText = cart.Image.AltText
                } : null
            };
        }

        private Cart ConvertCartViewModelToCart(CartViewModel cartViewModel)
        {
            return new Cart
            {
                Id = cartViewModel.Id,
                Name = cartViewModel.Name,
                Price = cartViewModel.Price,
                Quantity = cartViewModel.Quantity,
                Image = cartViewModel.Image is not null ? new Image
                {
                    Url = cartViewModel.Image.Url,
                    AltText = cartViewModel.Image.AltText
                } : null
            };
        }
    }
}
