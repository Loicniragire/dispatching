namespace Ordering.UnitTests.Domain;

[TestFixture]
public class OrderTests
{
    [Test]
    public void Instantiate_Order_ShouldAddOrderStarterDomainEventToDomainEventsList()
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
}
