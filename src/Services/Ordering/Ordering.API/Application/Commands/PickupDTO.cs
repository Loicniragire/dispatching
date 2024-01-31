namespace Ordering.API.Application.DTOs;
public class PickupDTO
{
       public LocationDTO PickupLocation { get; init; }
       public DateTimeOffset PickupTime { get; init; }
}
