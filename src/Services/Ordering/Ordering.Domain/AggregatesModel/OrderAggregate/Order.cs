using Ordering.Domain.Events;
using Ordering.Domain.Exceptions;
using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregatesModel.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    private DateTime _orderDate;
    private string _description;
    private int? _clientId;
    private int _orderStatusId;
    private Delivery _delivery;
    private readonly List<Load> _orderItems;

    public Address PickupAddress { get; private set; }
    public Address DropoffAddress { get; private set; }
    public OrderStatus OrderStatus { get { return OrderStatus.From(_orderStatusId); } }
    public int? ClientId => _clientId;
    public IReadOnlyCollection<Load> OrderItems => _orderItems;

    protected Order()
    {
        _orderItems = new List<Load>();
    }

    public Order(string userId,
                     string userName,
                     Address pickupAddress,
                     Address dropoffAddress,
                     DateTimeOffset orderDate,
                     string description) : this()
    {
        _orderStatusId = OrderStatus.Submitted.Id;
        _orderDate = DateTime.UtcNow;
        PickupAddress = pickupAddress;
        DropoffAddress = dropoffAddress;

        // Add the OrderStarterDomainEvent to the domain events collection 
        // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
        AddOrderStartedDomainEvent(userId, userName);
    }

    // DDD Patterns comment
    // This Order AggregateRoot's method "AddOrderitem()" should be the only way to add Items to the Order,
    // so any behavior (discounts, etc.) and validations are controlled by the AggregateRoot 
    // in order to maintain consistency between the whole Aggregate. 
    public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, int units = 1)
    {
        var existingOrderForProduct = _orderItems.Where(o => o.ProductId == productId)
            .SingleOrDefault();

        if (existingOrderForProduct != null)
        {
            if (discount > existingOrderForProduct.GetCurrentDiscount())
            {
                existingOrderForProduct.SetNewDiscount(discount);
            }

            existingOrderForProduct.AddUnits(units);
        }
        else
        {
            //add validated new order item
            var orderItem = new Load(productId, productName, unitPrice, discount, units);
            _orderItems.Add(orderItem);
        }
    }

    public void SetClientId(int id)
    {
        _clientId = id;
    }

    public void SetAwaitingValidationStatus()
    {
        if (_orderStatusId == OrderStatus.Submitted.Id)
        {
            AddDomainEvent(new OrderStatusChangedToAwaitingValidationDomainEvent(Id, _orderItems));
            _orderStatusId = OrderStatus.AwaitingValidation.Id;
        }
    }

    public void SetConfirmedStatus()
    {
        if (_orderStatusId == OrderStatus.AwaitingValidation.Id)
        {
            AddDomainEvent(new OrderStatusChangedToConfirmedDomainEvent(Id));

            _orderStatusId = OrderStatus.Confirmed.Id;
            _description = "The order was confirmed.";
        }
    }

    public void SetPickedUpStatus(string deliveryRoute)
    {
        if (_orderStatusId == OrderStatus.Confirmed.Id)
        {
            AddDomainEvent(new OrderStatusChangedToPickedUpDomainEvent(Id));

            _orderStatusId = OrderStatus.PickedUp.Id;
            _description = "The order was picked up.";
            _delivery = new Delivery(deliveryRoute);
        }
    }

    public void SetInTransitStatus()
    {
        if (_orderStatusId == OrderStatus.PickedUp.Id)
        {
            AddDomainEvent(new OrderStatusChangedToInTransitDomainEvent(Id));

            _orderStatusId = OrderStatus.InTransit.Id;
            _description = "The order is in-transit.";
        }
    }

    public void SetDeliveredStatus()
    {
        if (_orderStatusId == OrderStatus.InTransit.Id)
        {
            AddDomainEvent(new OrderStatusChangedToDeliveredDomainEvent(Id));

            _orderStatusId = OrderStatus.Delivered.Id;
            _description = "The order has been Delivered.";
        }
    }

    public void SetPaidStatus()
    {
        if (_orderStatusId == OrderStatus.Delivered.Id)
        {
            AddDomainEvent(new OrderStatusChangedToPaidDomainEvent(Id, OrderItems));

            _orderStatusId = OrderStatus.Paid.Id;
            _description = "Order was paid.";
        }
    }

    public void SetCancelledStatus()
    {
        if (_orderStatusId == OrderStatus.PickedUp.Id ||
            _orderStatusId == OrderStatus.InTransit.Id ||
            _orderStatusId == OrderStatus.Paid.Id ||
            _orderStatusId == OrderStatus.Delivered.Id)
        {
            StatusChangeException(OrderStatus.Cancelled);
        }

        _orderStatusId = OrderStatus.Cancelled.Id;
        _description = $"The order was cancelled.";
        AddDomainEvent(new OrderCancelledDomainEvent(this));
    }

    public decimal GetTotal()
    {
        return _orderItems.Sum(o => o.GetUnits() * o.GetUnitPrice());
    }

    public void CompleteDelivery(decimal gasCost, decimal tollsCost, decimal additionalCosts, double endOdometer)
    {
        if (_delivery == null) throw new MissingMemberException("The delivery is missing.");

        _delivery.CompleteDelivery(gasCost, tollsCost, additionalCosts, endOdometer);
        this.AddDomainEvent(new DeliveryCompletedDomainEvent(Id, endOdometer, _delivery.Id));
    }

    public void StartDelivery(double startOdometer)
    {
        if (_delivery == null) throw new MissingMemberException("The delivery is missing.");

        _delivery.StartDelivery(startOdometer);
        this.AddDomainEvent(new DeliveryStartedDomainEvent(Id, startOdometer, _delivery.Id));
    }

    private void AddOrderStartedDomainEvent(string userId, string userName)
    {
        var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName);
        this.AddDomainEvent(orderStartedDomainEvent);
    }

    private void StatusChangeException(OrderStatus orderStatusToChange)
    {
        throw new OrderingDomainException($"Is not possible to change the order status from {OrderStatus.Name} to {orderStatusToChange.Name}.");
    }
}
