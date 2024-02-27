namespace Ordering.Domain.Events;

public class ClientRemovedDomainEvent : INotification
{
	public string IdentityGuid { get; }

	public ClientRemovedDomainEvent(string identityGuid)
	{
		IdentityGuid = identityGuid;
	}
}
