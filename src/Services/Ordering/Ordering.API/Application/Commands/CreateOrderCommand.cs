using MediatR;
using Ordering.API.Application.DTOs;

namespace Ordering.API.Application.Commands
{
    public class CreateOrderCommand: IRequest<bool>
    {
		public string ClientId { get; init; }
		public string ClientName { get; init; }
		public PickupDTO Pickup{ get; init; }
		public DropoffDTO DropOff{ get; init; }

    }

}

