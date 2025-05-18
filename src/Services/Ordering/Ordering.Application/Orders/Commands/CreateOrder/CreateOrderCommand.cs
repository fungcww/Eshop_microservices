using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order)
    : ICommand<CreateOrderResult>;
    
    public record CreateOrderResult(Guid Id);

    public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName)
                .NotEmpty()
                .WithMessage("Order name cannot be empty.");

            RuleFor(x => x.Order.CustomerId)
                .NotNull()
                .WithMessage("CustomerId cannot be empty.");

            RuleFor(x => x.Order.OrderItems)
                .NotEmpty()
                .WithMessage("Order items cannot be empty.");
        }
    }
}
