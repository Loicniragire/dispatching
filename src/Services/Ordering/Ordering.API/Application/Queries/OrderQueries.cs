using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Queries;

public class OrderQueries : IOrderQueries
{
    public Task<Order> GetOrderAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }
}
