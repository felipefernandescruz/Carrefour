using Carrefour.Management.Application.OrderApplication.Models.Dto;
using FluentValidation;

namespace Carrefour.Management.Application.OrderApplication.Validator
{
    public class OrderValidator : AbstractValidator<OrderDTO>
    {
        public OrderValidator()
        {
            RuleFor(x => x.TotalOrder).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
