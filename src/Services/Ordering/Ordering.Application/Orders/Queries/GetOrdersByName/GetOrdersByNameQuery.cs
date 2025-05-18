namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public record GetOrdersByCustomerQuery(string Name) 
        : IQuery<GetOrdersByNameResult>;

    public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);
}
