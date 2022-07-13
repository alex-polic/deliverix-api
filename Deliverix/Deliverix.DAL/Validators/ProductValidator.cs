using Deliverix.DAL.Models;
using FluentValidation;

namespace Deliverix.DAL.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(e => e.Name).NotNull().NotEmpty().WithMessage("Name is mandatory field");
        RuleFor(e => e.Price).NotNull().NotEmpty().WithMessage("Price is mandatory field");
    }
}