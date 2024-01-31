namespace Ordering.API.Application.DTOs;
public class DropoffDTO
{
       public PickupDTO Pickup { get; init; }
       public LocationDTO DropoffLocation { get; init; }
       public DateTimeOffset DropoffTime { get; init; }

}

