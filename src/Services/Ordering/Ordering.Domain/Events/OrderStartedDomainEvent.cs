using MediatR;

namespace Ordering.Domain.Events;

public class OrderStartedDomainEvent : INotification
{
    public string UserId { get; }
    public string UserName { get; }
    public int OrderId { get; }
    public string Description { get; }
    public decimal Total { get; }
    public string PickupStreet { get; }
    public string PickupCity { get; }
    public string PickupState { get; }
    public string PickupZip { get; }
    public string DropoffStreet { get; }
    public string DropoffCity { get; }
    public string DropoffState { get; }
    public string DropoffZip { get; }

    public OrderStartedDomainEvent(string userId,
                                       string userName,
                                       int orderId,
                                       string description,
                                       decimal total,
                                       string pickupStreet,
                                       string pickupCity,
                                       string pickupState,
                                       string pickupZip,
                                       string dropoffStreet,
                                       string dropoffCity,
                                       string dropoffState,
                                       string dropoffZip)
    {
        UserId = userId;
        UserName = userName;
        OrderId = orderId;
        Description = description;
        Total = total;
        PickupStreet = pickupStreet;
        PickupCity = pickupCity;
        PickupState = pickupState;
        PickupZip = pickupZip;
        DropoffStreet = dropoffStreet;
        DropoffCity = dropoffCity;
        DropoffState = dropoffState;
        DropoffZip = dropoffZip;
    }
}


