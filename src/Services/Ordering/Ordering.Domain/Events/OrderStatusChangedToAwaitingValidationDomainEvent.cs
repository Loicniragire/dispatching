namespace Ordering.Domain.Events;

public class OrderStatusChangedToAwaitingValidationDomainEvent : INotification
{
    public OrderStatusChangedToAwaitingValidationDomainEvent(int orderId, IEnumerable<Load> orderItems)
    {
        OrderId = orderId;
        OrderItems = orderItems;
    }
    public int OrderId { get; }
    public IEnumerable<Load> OrderItems { get; }
}
