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

    [DataMember]
    public DateTimeOffset OrderDate { get; init; }

    [DataMember]
    public string Description { get; init; }

    public CreateOrderCommand(string clientId, string clientName, IEnumerable<LoadDTO> loads, DateTimeOffset orderDate, string description) : this()
    {
        ClientId = clientId;
        ClientName = clientName;
        OrderDate = orderDate;
        Description = description;
    }

    public CreateOrderCommand()
    {
        _loads = new List<LoadDTO>();
    }
}
