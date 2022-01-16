using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("{UserName} is required.").NotNull().MaximumLength(50).WithMessage("{UserName must not exceed 50 characters.}");

            RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("{Email Address} is required");

            RuleFor(p => p.TotalPrice).NotEmpty().WithMessage("{Total Price} is required").GreaterThan(0).WithMessage("{Total Price} should be greater than zero.");
        }
    }
}