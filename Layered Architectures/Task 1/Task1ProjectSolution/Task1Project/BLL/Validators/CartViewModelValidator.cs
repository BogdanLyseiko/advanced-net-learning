using FluentValidation;
using Task1Project.BLL.ViewModels;
using Task1Project.DAL.Entities;
using Task1Project.DAL.Repositories.Interfaces;

namespace Task1Project.BLL.Validators
{
    public class CartViewModelValidator : AbstractValidator<CartViewModel>
    {
        public CartViewModelValidator(IRepository<Cart> repository)
        {
            RuleFor(cart => cart.Id).NotEmpty().Custom((x, y) =>
            {
                Cart? existingCart = repository.Get(x);

                if (existingCart != null)
                {
                    y.AddFailure("Id should be unique!");
                }
            });

            RuleFor(cart => cart.Name).NotEmpty();
            RuleFor(cart => cart.Price).GreaterThan(0);
            RuleFor(cart => cart.Quantity).GreaterThan(0);
        }
    }
}
