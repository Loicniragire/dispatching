using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.SeedWork;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    public OrderRepository()
    {
    }

    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Order Add(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public void Update(Order order)
    {
        throw new NotImplementedException();
    }
}
