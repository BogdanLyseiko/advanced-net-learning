using Task1Project.BLL.ViewModels;

namespace Task1Project.BLL.Interfaces
{
    public interface ICartService
    {
        List<CartViewModel> GetAll();

        int Create(CartViewModel entity);

        bool Delete(int id);
    }
}
