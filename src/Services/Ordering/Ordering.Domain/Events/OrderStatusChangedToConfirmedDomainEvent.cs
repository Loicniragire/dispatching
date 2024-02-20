namespace Ordering.Domain.Events;

public class OrderStatusChangedToConfirmedDomainEvent : INotification
{
    public int OrderId { get; }

    public OrderStatusChangedToConfirmedDomainEvent(int orderId)
    {
        OrderId = orderId;
    }
}
