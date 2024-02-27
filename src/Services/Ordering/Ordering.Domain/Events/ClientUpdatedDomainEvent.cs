namespace Ordering.Domain.Events;

public class ClientUpdatedDomainEvent : INotification
{
    public string IdentityGuid { get; }
    public string Name { get; }
    public string Email { get; }
    public string Phone { get; }
    public string Notes { get; }

    public ClientUpdatedDomainEvent(string identityGuid, string name, string email, string phone, string notes)
    {
        IdentityGuid = identityGuid;
        Name = name;
        Email = email;
        Phone = phone;
        Notes = notes;
    }
}
