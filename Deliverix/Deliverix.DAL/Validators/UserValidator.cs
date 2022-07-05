using Deliverix.DAL.Models;
using FluentValidation;

namespace Deliverix.DAL.Validators;

public class UserValidator : AbstractValidator<User?> 
{
    public UserValidator()
    {
        RuleFor(user => user.Email)
            .NotNull()
            .EmailAddress()
            .WithMessage("Email is not in right format");
        RuleFor(user => user.Password)
            .MinimumLength(8)
            .WithMessage("Password must be more than 8 characters");
        RuleFor(user => user.Address).NotNull();
        RuleFor(user => user.FullName).NotNull();
        RuleFor(user => user.DateOfBirth).NotNull();
        RuleFor(user => user.UserType).NotNull();
        RuleFor(user => user.VerificationStatus).NotNull();
    }
}