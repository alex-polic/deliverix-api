using Deliverix.DAL.Models;
using FluentValidation;

namespace Deliverix.DAL.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(e => e.BuyerId).NotNull().GreaterThan(0).WithMessage("Buyer is mandatory field");
        RuleFor(e => e.DeliveryAddress).NotEmpty().WithMessage("Delivery address is mandatory field");
    }
}