using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ordering.Application.Features.Order.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        { // we can define rules for username and it must be used before command
            RuleFor(p => p.UserName).NotEmpty().WithMessage("{UserName} is required.").NotNull().MaximumLength(50).WithMessage("{UserName must not exceeds 50 characters.}");
            RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("{EmailAddress} is required");
            RuleFor(p => p.TotalPrice).NotEmpty().WithMessage("{TotalPrice} is required.").GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");

        }
    }
}
