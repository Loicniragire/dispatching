namespace Ordering.API.Application.Commands;

[DataContract]
public class CreateOrderCommand : IRequest<bool>
{
	[DataMember]
	private readonly List<LoadDTO> _loads;

	[DataMember]
    public string ClientId { get; init; }

	[DataMember]
    public string ClientName { get; init; }

	[DataMember]
	public IEnumerable<LoadDTO> Loads => _loads;

	public CreateOrderCommand(string clientId, string clientName, IEnumerable<LoadDTO> loads): this()
	{
		ClientId = clientId;
		ClientName = clientName;
	}

	public CreateOrderCommand()
	{
		_loads = new List<LoadDTO>();
	}
}
