using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
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
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            //var orderDtos = ProjectToOrderDto(orders);

            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
