namespace Ordering.Domain.Events;

public class OrderStatusChangedToPaidDomainEvent : INotification
{
    public OrderStatusChangedToPaidDomainEvent(int orderId, IEnumerable<Load> orderItems)
    {
        OrderId = orderId;
        OrderItems = orderItems;
    }

    public int OrderId { get; }
    public IEnumerable<Load> OrderItems { get; }
}
