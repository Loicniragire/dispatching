namespace Ordering.Domain.AggregatesModel.ClientAggregate;

public class Client : Entity, IAggregateRoot
{
    public string IdentityGuid { get; private set; }
    public string Name { get; private set; }
    public int? AddressId { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Notes { get; private set; }
    public bool IsRemoved { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    public DateTime? RemovedDate { get; private set; }

	// Navigation properties
	public Address Address { get; private set; }
	public ICollection<Order> Orders { get; set; }

    public Client(string identityGuid, string name, string email, string phone, string notes, Address address)
    {
        IdentityGuid = identityGuid;
        Name = name;
        Email = email;
        Phone = phone;
        Notes = notes;
        Address = address;
        CreatedDate = DateTime.UtcNow;
        IsRemoved = false;
        AddClientCreatedDomainEvent(identityGuid, name, email, phone, notes);
    }

    public void Update(string name, string email, string phone, string notes)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Notes = notes;
        UpdatedDate = DateTime.UtcNow;
        AddClientUpdatedDomainEvent(IdentityGuid, name, email, phone, notes);
    }

    public void Remove()
    {
        IsRemoved = true;
        RemovedDate = DateTime.UtcNow;
        AddClientRemovedDomainEvent(IdentityGuid);
    }

    private void AddClientRemovedDomainEvent(string identityGuid)
    {
		var clientRemovedDomainEvent = new ClientRemovedDomainEvent(identityGuid);
		this.AddDomainEvent(clientRemovedDomainEvent);
    }

    private void AddClientCreatedDomainEvent(string identityGuid, string name, string email, string phone, string notes)
    {
        var clientCreatedDomainEvent = new ClientCreatedDomainEvent(identityGuid, name, email, phone, notes);
        this.AddDomainEvent(clientCreatedDomainEvent);
    }

    private void AddClientUpdatedDomainEvent(string identityGuid, string name, string email, string phone, string notes)
    {
        var clientUpdatedDomainEvent = new ClientUpdatedDomainEvent(identityGuid, name, email, phone, notes);
        this.AddDomainEvent(clientUpdatedDomainEvent);
    }
}
