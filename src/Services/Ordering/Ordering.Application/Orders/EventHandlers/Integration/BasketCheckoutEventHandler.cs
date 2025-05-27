using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler
        (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            //TODO: Create new order and start order fullfillment process
            logger.LogInformation("Integration Event: {IntegrationEvent} is consumed successfully", context.Message.GetType().Name);

            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }
        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            // Create full order with incoming event data
            var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
            var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
            var orderId = Guid.NewGuid();

            var orderDto = new OrderDto(
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto, // Assuming same address for shipping and billing
                Payment: paymentDto,
                Status: Ordering.Domain.Enums.OrderStatus.Pending, // Default status
                OrderItems:
                [
                    new OrderItemDto(orderId, new Guid("5e42bee4-3d4f-4c8a-bb74-684831073cd6"), 2, 500),
                    new OrderItemDto(orderId, new Guid("bc8d4a1b-c1ff-4f90-8bb3-9035f4724fc2"), 1, 400)
                ]);

            return new CreateOrderCommand(orderDto);
        }
    }
}
