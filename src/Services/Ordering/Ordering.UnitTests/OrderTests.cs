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
        Assert.AreEqual(1, order.OrderItems.Count());
        var orderItem = order.OrderItems.Single();
        Assert.AreEqual(productId, orderItem.ProductId);
        // Also checking that the number of units was increased to 4 (2 units originally + 2 additional units)
        Assert.AreEqual(4, orderItem.GetUnits());
    }
}
