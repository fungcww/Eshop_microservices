using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
        new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("619eeaee-34ec-4833-8c69-911d7b071d16")), "wai2", "wai2@gmail.com"),
            Customer.Create(CustomerId.Of(new Guid("8916c2e7-f4bf-4d73-982d-f502dbe8c2ec")), "wai", "wai@gmail.com")
        };

        public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(ProductId.Of(new Guid("70c66507-db1b-4f3e-a1be-2206daca67f4")), "Iphone X", 100),
            Product.Create(ProductId.Of(new Guid("e936b529-9aa0-4948-9c67-7a18b070710f")), "Samsung 10", 50),
            Product.Create(ProductId.Of(new Guid("f8b5eff1-5476-4606-97af-ff3d99e2ff56")), "Huawei", 150)
        };
        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Of("wai2", "wai23", "wai2@gmail.com", "hk4", "hk3", "hk2", "14f");
                var address2 = Address.Of("wai", "fung", "wai@gmail.com", "pl4", "pl3", "pl2", "7f");

                var payment1 = Payment.Of("wai2", "1111111111112222", "11/28", "123", 1);
                var payment2 = Payment.Of("wai", "2222222222223333", "06/29", "133", 2);

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("619eeaee-34ec-4833-8c69-911d7b071d16")),
                    OrderName.Of("ORD_1"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment1);
                order1.Add(ProductId.Of(new Guid("70c66507-db1b-4f3e-a1be-2206daca67f4")), 2, 500);
                order1.Add(ProductId.Of(new Guid("e936b529-9aa0-4948-9c67-7a18b070710f")), 1, 400);

                var order2 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("8916c2e7-f4bf-4d73-982d-f502dbe8c2ec")),
                    OrderName.Of("ORD_2"),
                    shippingAddress: address2,
                    billingAddress: address2,
                    payment2);
                order2.Add(ProductId.Of(new Guid("70c66507-db1b-4f3e-a1be-2206daca67f4")), 3, 600);
                order2.Add(ProductId.Of(new Guid("e936b529-9aa0-4948-9c67-7a18b070710f")), 4, 700);

                return new List<Order> { order1, order2 };
            }
        }
    }
}