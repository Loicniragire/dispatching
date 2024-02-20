namespace Ordering.Domain.Events;

public class OrderStatusChangedToPickedUpDomainEvent : INotification
{
    public int OrderId { get; }

    public OrderStatusChangedToPickedUpDomainEvent(int orderId)
    {
        OrderId = orderId;
    }
}
