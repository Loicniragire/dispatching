namespace Ordering.Domain.AggregatesModel.ClientAggregate;

public class Client: Entity, IAggregateRoot
{
    public string IdentityGuid { get; private set; }
    public string Name { get; private set; }
}
