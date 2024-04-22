namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderingContext _orderContext;
    public IUnitOfWork UnitOfWork => _orderContext;

    public OrderRepository(OrderingContext orderContext)
    {
        _orderContext = orderContext ?? throw new ArgumentNullException(nameof(orderContext));
    }


    public Order Add(Order order)
    {
        return _orderContext.Orders.Add(order).Entity;
    }

    public async Task<Order> GetAsync(int orderId)
    {
        var order = await _orderContext.Orders
                   .Include(o => o.PickupAddress)
                   .Include(o => o.DropoffAddress)
                   .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            order = _orderContext.Orders.Local.FirstOrDefault(o => o.Id == orderId);
        }

        if (order != null)
        {
            await _orderContext.Entry(order).Collection(i => i.OrderItems).LoadAsync();
            await _orderContext.Entry(order).Reference(i => i.OrderStatus).LoadAsync();
        }
        return order;
    }

    public void Update(Order order)
    {
        _orderContext.Entry(order).State = EntityState.Modified;
    }
}
