using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events
{
    public record BasketCheckoutEvent : IntegrationEvent
    {
        public string UserName { get; set; } = default!;
        public Guid CustomerId { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;
        // Shipping and BillingAddress
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string EmailAddress { get; init; } = default!;
        public string AddressLine { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string State { get; init; } = default!;
        public string ZipCode { get; init; } = default!;

        // Payment
        public string CardName { get; init; } = default!;
        public string CardNumber { get; init; } = default!;
        public string Expiration { get; init; } = default!;
        public string CVV { get; init; } = default!;
        public int PaymentMethod { get; init; } = default!;
    }
}
