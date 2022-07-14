using Deliverix.DAL.Models;
using FluentValidation;

namespace Deliverix.DAL.Validators;

public class OrderedProductValidator : AbstractValidator<OrderedProduct>
{
    public OrderedProductValidator()
    {
        RuleFor(e => e.OrderId).NotNull().WithMessage("Order is mandatory field");
        RuleFor(e => e.ProductId).NotNull().WithMessage("Product is mandatory field");
        RuleFor(e => e.Amount).NotNull().GreaterThan(0).WithMessage("Amount is mandatory field");
    }
}