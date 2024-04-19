namespace Ordering.API.Application.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator, ILogger<CreateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(CreateOrderCommand message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateOrderCommand");
        var clientId = message.ClientId;
        var clientName = message.ClientName;
        var loads = message.Loads;

        foreach (var load in message.Loads)
        {
            var pickupAddress = new Ordering.Domain.AggregatesModel.OrderAggregate.Address(
                street: load.Pickup.PickupLocation.Street,
                city: load.Pickup.PickupLocation.City,
                state: load.Pickup.PickupLocation.State,
                country: "USA",
                zipcode: load.Pickup.PickupLocation.ZipCode);

            var dropoff = new Ordering.Domain.AggregatesModel.OrderAggregate.Address(
                street: load.DropOff.DropoffLocation.Street,
                city: load.DropOff.DropoffLocation.City,
                state: load.DropOff.DropoffLocation.State,
                country: "USA",
                zipcode: load.DropOff.DropoffLocation.ZipCode);

            var pounds = load.Pounds;
            var pricePerUnit = load.PricePerUnit;
            var notes = load.Notes;

            var order = new Ordering.Domain.AggregatesModel.OrderAggregate.Order(
                userId: clientId,
                userName: clientName,
                pickupAddress: pickupAddress,
                dropoffAddress: dropoff,
                orderDate: message.OrderDate,
                description: message.Description);

            try
            {
                _orderRepository.Add(order);
                await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
				// TODO: Publish OrderStartedIntegrationEvent
                await _mediator.Publish(new OrderStartedIntegrationEvent(order.Id), cancellationToken);
                _logger.LogInformation("Order {OrderId} was successfully created", order.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR handling CreateOrderCommand: {Message}", ex.Message);
                return false;
            }
        };



    }

}

public class CreateOrderIdentifiedCommandHandler : IdentifiedCommandHandler<CreateOrderCommand, bool>
{
    public CreateOrderIdentifiedCommandHandler(IMediator mediator,
                                                   IRequestManager requestManager,
                                                   ILogger<IdentifiedCommandHandler<CreateOrderCommand, bool>> logger
                                                  ) : base(mediator, requestManager, logger)
    {
    }

    protected override bool CreateResultForDuplicateRequest()
    {
        return true; // Ignore duplicate requests for creating order.
    }
}

/*
 *


using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Infrastructure;

namespace Ordering.API.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
        }
    }
}
 *
 */
