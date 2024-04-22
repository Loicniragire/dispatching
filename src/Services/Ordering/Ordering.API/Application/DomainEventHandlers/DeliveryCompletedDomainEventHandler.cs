namespace Ordering.API.Application.DomainEventHandlers;

public class DeliveryCompletedDomainEventHandler : INotificationHandler<DeliveryCompletedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILoggerFactory _logger;

    public DeliveryCompletedDomainEventHandler(IOrderRepository orderRepository, ILoggerFactory logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task Handle(DeliveryCompletedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger
            .CreateLogger<DeliveryCompletedDomainEventHandler>()
            .LogTrace("Delivery Completed Event received for order with Id: {OrderId}", notification.OrderId);
    }
}

