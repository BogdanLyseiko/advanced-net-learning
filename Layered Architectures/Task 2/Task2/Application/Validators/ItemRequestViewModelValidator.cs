using Application.ViewModels;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;

namespace Application.Validators
{
    public class ItemRequestViewModelValidator : AbstractValidator<ItemRequestViewModel>
    {
        public ItemRequestViewModelValidator(IRepository<Category> categoryRepo)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.CategoryId).Must((id) =>
            {
                Category? existingCategory = categoryRepo.GetAsync(id).Result;

                return existingCategory != null;
            }).WithMessage("Category does not exsist");
        }
    }
}
