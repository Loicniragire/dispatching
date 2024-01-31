namespace Ordering.API.Application.DTOs;

public class LoadDTO
{
    public PickupDTO Pickup { get; init; }
    public DropoffDTO DropOff { get; init; }
	public decimal Pounds { get; init; }
	public decimal PricePerUnit { get; init; }
	public string Notes { get; init; }
}
