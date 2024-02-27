namespace Ordering.Domain.AggregatesModel.ClientAggregate;

public class Client: Entity, IAggregateRoot
{
    public string IdentityGuid { get; private set; }
    public string Name { get; private set; }
	public int? AddressId { get; private set; }
	public Address Address { get; private set; }
	public string Email { get; private set; }
	public string Phone { get; private set; }
	public string Notes { get; private set; }
	public bool IsRemoved { get; private set; }
	public DateTime CreatedDate { get; private set; }
	public DateTime? UpdatedDate { get; private set; }
	public DateTime? RemovedDate { get; private set; }

	public Client(string identityGuid, string name, string email, string phone, string notes, int? addressId)
	{
		IdentityGuid = identityGuid;
		Name = name;
		Email = email;
		Phone = phone;
		Notes = notes;
		AddressId = addressId;
		CreatedDate = DateTime.UtcNow;
	}

	public void Update(string name, string email, string phone, string notes, int? addressId)
	{
		Name = name;
		Email = email;
		Phone = phone;
		Notes = notes;
		AddressId = addressId;
		UpdatedDate = DateTime.UtcNow;
	}

	public void Remove()
	{
		IsRemoved = true;
		RemovedDate = DateTime.UtcNow;
	}
}
