﻿namespace Shopping.Web.Services
{
    public interface IOrderingServicecs
    {
        [Get("/ordering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}\"")]
        Task<GetOrdersResponse> GetOrders(int? pageIndex = 1, int? pageSize = 10);
        [Get("/ordering-service/orders/{orderName}")]
        Task<GetOrderByNameResponse> GetOrdersByName(string orderName);
        [Get("/ordering-service/orders/customer/{customerId}")]
        Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(Guid customerId);
    }
}
