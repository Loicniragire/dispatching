namespace Ordering.Domain.Events;

public class DeliveryCompletedDomainEvent: INotification
{
	public int OrderId { get; }
	public double Odometer { get; }
	public int DeliveryId { get; }

	public DeliveryCompletedDomainEvent(int orderId, double odometer, int deliveryId)
	{
		OrderId = orderId;
		Odometer = odometer;
		DeliveryId = deliveryId;
	}
}


