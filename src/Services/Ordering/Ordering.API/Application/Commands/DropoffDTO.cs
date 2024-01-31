namespace Ordering.API.Application.DTOs;
public class DropoffDTO
{
    public LocationDTO DropoffLocation { get; init; }
    public DateTimeOffset DropoffTime { get; init; }
}
