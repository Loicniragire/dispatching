namespace Ordering.API.Application.IntegrationEvents;

public record OrderStartedIntegrationEvent : IntegrationEvent
{
	public int OrderId { get; }

	public OrderStartedIntegrationEvent(int orderId)
	{
		OrderId = orderId;
	}
}

