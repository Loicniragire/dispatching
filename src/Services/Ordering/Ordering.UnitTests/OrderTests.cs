using Ordering.Domain.Exceptions;

namespace Ordering.UnitTests.Domain;

[TestFixture]
public class OrderTests
{
    [Test]
    public void Instantiating_Order_ShouldAddOrderStarterDomainEventToDomainEventsList()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");

        // Act
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        // Assert
        var domainEvent = order.DomainEvents.FirstOrDefault(ev => ev is OrderStartedDomainEvent);
        Assert.IsNotNull(domainEvent, "Domain event was not added.");
        Assert.IsInstanceOf<OrderStartedDomainEvent>(domainEvent, "The domain event is not of type OrderStartedDomainEvent.");
        Assert.That(order.OrderItems.Count, Is.EqualTo(0));
        // Assert that order is in Submitted status
        Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Submitted));
    }

    [Test]
    public void AddOrderItem_ShouldNotAddDuplicateOrderItems()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");

        // Act
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");
        int productId = 1;
        string productName = "Test Product";
        decimal unitPrice = 10.0m;
        decimal discount = 0.0m;
        int units = 2;

        // Add an order item
        order.AddOrderItem(productId, productName, unitPrice, discount, units);

        // Act
        // Attempt to add the same order item again
        order.AddOrderItem(productId, productName, unitPrice, discount, units);

        // Assert
        // Confirm that a duplicate was not added.
        Assert.That(order.OrderItems.Count, Is.EqualTo(1));
        var orderItem = order.OrderItems.Single();
        Assert.That(orderItem.ProductId, Is.EqualTo(productId));
        // Also checking that the number of units was increased to 4 (2 units originally + 2 additional units)
        Assert.That(orderItem.GetUnits, Is.EqualTo(4));
    }

    [Test]
    public void AddOrderItem_WhenAddingDuplicateWithHigherDiscount_ShouldUpdateDiscount()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        int productId = 1;
        string productName = "Test Product";
        decimal unitPrice = 10.0m;
        decimal initialDiscount = 0.1m;
        decimal higherDiscount = 0.2m;
        int units = 2;

        // Add an order item with initial discount
        order.AddOrderItem(productId, productName, unitPrice, initialDiscount, units);

        // Act
        // Add the same order item with a higher discount
        order.AddOrderItem(productId, productName, unitPrice, higherDiscount, units);

        // Assert
        var orderItem = order.OrderItems.Single();
        Assert.AreEqual(higherDiscount, orderItem.GetCurrentDiscount(), "The discount should be updated to the higher value");
    }

    [Test]
    public void SetAwaitingValidationStatus_ShouldAddOrderStatusChangedToAwaitingValidationDomainEvent()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        // Act
        order.SetAwaitingValidationStatus();

        // Assert
        var domainEvent = order.DomainEvents.FirstOrDefault(ev => ev is OrderStatusChangedToAwaitingValidationDomainEvent);
        Assert.IsNotNull(domainEvent, "Domain event was not added.");
        Assert.IsInstanceOf<OrderStatusChangedToAwaitingValidationDomainEvent>(domainEvent, "The domain event is not of type OrderStatusChangedToAwaitingValidationDomainEvent.");
    }

    [Test]
    public void SetConfirmedStatus_ShouldAddOrderStatusChangedToConfirmedDomainEvent()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        // Order needs to be in AwaitingValidation status before it can be set to Confirmed status
        order.SetAwaitingValidationStatus();

        // Act
        order.SetConfirmedStatus();

        // Assert
        var domainEvent = order.DomainEvents.FirstOrDefault(ev => ev is OrderStatusChangedToConfirmedDomainEvent);
        Assert.IsNotNull(domainEvent, "Domain event was not added.");
        Assert.IsInstanceOf<OrderStatusChangedToConfirmedDomainEvent>(domainEvent, "The domain event is not of type OrderStatusChangedToStockConfirmedDomainEvent.");
    }

    [Test]
    public void SetPickedupStatus_ShouldAddOrderStatusChangedToPickedUpDomainEvent()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        // Act
        // Propagate the Order to PickedUp status. 
        // Sequence order matters. Order needs to be in Confirmed status before it can be set to PickedUp status
        order.SetAwaitingValidationStatus();
        order.SetConfirmedStatus();
        order.SetPickedUpStatus();

        // Assert
        // Confirm that the domain event was added
        var domainEvent = order.DomainEvents.FirstOrDefault(ev => ev is OrderStatusChangedToPickedUpDomainEvent);
        Assert.IsNotNull(domainEvent, "Domain event was not added.");
        Assert.IsInstanceOf<OrderStatusChangedToPickedUpDomainEvent>(domainEvent, "The domain event is not of type OrderStatusChangedToPickedUpDomainEvent.");
    }

    [Test]
    public void SetInTransitStatus_ShouldAddOrderStatusChangedToInTransitDomainEvent()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        // Act
        // Propagate the Order to InTransit status.
        // Sequence order matters. Order needs to be in PickedUp status before it can be set to InTransit status
        order.SetAwaitingValidationStatus();
        order.SetConfirmedStatus();
        order.SetPickedUpStatus();
        order.SetInTransitStatus();

        // Assert
        // Confirm that the domain event was added
        var domainEvent = order.DomainEvents.FirstOrDefault(ev => ev is OrderStatusChangedToInTransitDomainEvent);
        Assert.IsNotNull(domainEvent, "Domain event was not added.");
        Assert.IsInstanceOf<OrderStatusChangedToInTransitDomainEvent>(domainEvent, "The domain event is not of type OrderStatusChangedToInTransitDomainEvent.");
    }

    [Test]
    public void SetDeliveredStatus_ShouldAddOrderStatusChangedToDeliveredDomainEvent()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        // Act
        // Propagate the Order to Delivered status.
        // Sequence order matters. Order needs to be in InTransit status before it can be set to Delivered status
        order.SetAwaitingValidationStatus();
        order.SetConfirmedStatus();
        order.SetPickedUpStatus();
        order.SetInTransitStatus();
        order.SetDeliveredStatus();

        // Assert
        // Confirm that the domain event was added
        var domainEvent = order.DomainEvents.FirstOrDefault(ev => ev is OrderStatusChangedToDeliveredDomainEvent);
        Assert.IsNotNull(domainEvent, "Domain event was not added.");
        Assert.IsInstanceOf<OrderStatusChangedToDeliveredDomainEvent>(domainEvent, "The domain event is not of type OrderStatusChangedToDeliveredDomainEvent.");

    }

    [Test]
    public void SetPaidStatus_ShouldAddOrderStatusChangedToPaidDomainEvent()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        // Act
        // Propagate the Order to Paid status.
        // Sequence order matters. Order needs to be in Delivered status before it can be set to Paid status
        order.SetAwaitingValidationStatus();
        order.SetConfirmedStatus();
        order.SetPickedUpStatus();
        order.SetInTransitStatus();
        order.SetDeliveredStatus();
        order.SetPaidStatus();

        // Assert
        // Confirm that the domain event was added
        var domainEvent = order.DomainEvents.FirstOrDefault(ev => ev is OrderStatusChangedToPaidDomainEvent);
        Assert.IsNotNull(domainEvent, "Domain event was not added.");
        Assert.IsInstanceOf<OrderStatusChangedToPaidDomainEvent>(domainEvent, "The domain event is not of type OrderStatusChangedToPaidDomainEvent.");
    }

    [Test]
    public void SetCancelledStatus_ShouldAddOrderStatusChangedToCancelledDomainEvent()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        // Act
        // Propagate the Order to Cancelled status.
        // Sequence order matters. Order needs to be in Submitted status before it can be set to Cancelled status
        order.SetCancelledStatus();

        // Assert
        // Confirm that the domain event was added
        var domainEvent = order.DomainEvents.FirstOrDefault(ev => ev is OrderCancelledDomainEvent);
        Assert.IsNotNull(domainEvent, "Domain event was not added.");
        Assert.IsInstanceOf<OrderCancelledDomainEvent>(domainEvent, "The domain event is not of type OrderStatusChangedToCancelledDomainEvent.");

    }

    [Test]
    public void SetCancelledStatus_WhenOrderIsInTransit_ShouldNotAddOrderStatusChangedToCancelledDomainEvent()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        // Act
        // Propagate the Order to InTransit status.
        // Sequence order matters. Order needs to be in PickedUp status before it can be set to InTransit status
        order.SetAwaitingValidationStatus();
        order.SetConfirmedStatus();
        order.SetPickedUpStatus();
        order.SetInTransitStatus();

        // Attempt to cancel the order
        // This should not be allowed
		Assert.Throws<OrderingDomainException>(() => order.SetCancelledStatus());

        // Assert
        // Confirm that the domain event was not added
        var domainEvent = order.DomainEvents.FirstOrDefault(ev => ev is OrderCancelledDomainEvent);
        Assert.IsNull(domainEvent, "Domain event was added.");

    }

    [Test]
    public void SetCancelledStatus_WhenOrderIsInPickedupStatus_ShouldNotAddOrderStatusChangedToCancelledDomainEvent()
    {
        // Arrange
        var userId = "some-user-id";
        var userName = "some-user-name";
        var pickupAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var dropoffAddress = new Address("Test Street", "Test City", "Test State", "Test Country", "Test ZipCode");
        var order = new Order(userId, userName, pickupAddress, dropoffAddress, DateTimeOffset.Now, "Test Description");

        // Act
        // Propagate the Order to InTransit status.
        // Sequence order matters. Order needs to be in PickedUp status before it can be set to InTransit status
        order.SetAwaitingValidationStatus();
        order.SetConfirmedStatus();
        order.SetPickedUpStatus();

        // Attempt to cancel the order
        // This should not be allowed
		Assert.Throws<OrderingDomainException>(() => order.SetCancelledStatus());

        // Assert
        // Confirm that the domain event was not added
        var domainEvent = order.DomainEvents.FirstOrDefault(ev => ev is OrderCancelledDomainEvent);
        Assert.IsNull(domainEvent, "Domain event was added.");

    }
}
