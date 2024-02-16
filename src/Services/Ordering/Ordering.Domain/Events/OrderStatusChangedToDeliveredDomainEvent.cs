using MediatR;
namespace Ordering.Domain.Events;

public class OrderStatusChangedToDeliveredDomainEvent : INotification
{
    public int OrderId { get; }

    public OrderStatusChangedToDeliveredDomainEvent(int orderId)
    {
        OrderId = orderId;
    }
}


