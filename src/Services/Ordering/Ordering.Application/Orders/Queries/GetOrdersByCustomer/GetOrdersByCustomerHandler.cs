namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            //get orders by name using dbContext
            //return result

            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking() 
                //Change tracking won't track changes to entities in this context
                //AsNoTracking use with read-only queries, improves performance by disabling change tracking
                //Change tracking is used for tracking changes to entities in the context
                //Save changes to the database
                .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            //var orderDtos = ProjectToOrderDto(orders);

            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }
    }
}
