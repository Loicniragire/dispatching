namespace Ordering.API.Application.IntegrationEvents;

public class OrderingIntegrationEventService : IOrderingIntegrationEventService
{
    public Task AddAndSaveEventAsync(IntegrationEvent evt)
    {
        throw new NotImplementedException();
    }

    public Task PublishThroughEventBusAsync(Guid transactionId)
    {
        throw new NotImplementedException();
    }
}

