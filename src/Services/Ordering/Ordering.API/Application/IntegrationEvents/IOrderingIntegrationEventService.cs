using EventBus.Events.IntegrationEvents;

namespace Ordering.API.Application.IntegrationEvents;

public interface IOrderingIntegrationEventService
{
	Task PublishThroughEventBusAsync(Guid transactionId);
	Task AddAndSaveEventAsync(IntegrationEvent evt);
}

