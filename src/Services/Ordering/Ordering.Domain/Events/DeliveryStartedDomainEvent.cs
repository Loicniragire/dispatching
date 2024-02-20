using MediatR;

namespace Ordering.Domain.Events;

public class DeliveryStartedDomainEvent: INotification
{
	public int OrderId { get; }
	public double Odometer { get; }
	public int DeliveryId { get; }

	public DeliveryStartedDomainEvent(int orderId, double odometer, int deliveryId)
	{
		OrderId = orderId;
		Odometer = odometer;
		DeliveryId = deliveryId;
	}
}

