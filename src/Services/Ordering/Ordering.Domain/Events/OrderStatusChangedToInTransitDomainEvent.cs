namespace Ordering.Domain.Events;

public class OrderStatusChangedToInTransitDomainEvent : INotification
{
    public int OrderId { get; }

    public OrderStatusChangedToInTransitDomainEvent(int orderId)
    {
        OrderId = orderId;
    }
}
