namespace Ordering.API.Application.Queries;

public record Address
{
	public string street { get; init; }
	public string city { get; init; }
	public string state { get; init; }
	public string zip { get; init; }
}

public enum LoadStatus
{
	New,
	Assigned,
	PickedUp,
	Delivered,
	Completed
}

public record Load
{
    public string productname { get; init; }
    public int units { get; init; }
    public double unitprice { get; init; }
    public Address pickup { get; init; }
	public Address dropoff { get; init; }
	public LoadStatus status { get; init; }
	public string description { get; init; }
	public DateTimeOffset pickupUtcTime { get; init; }
	public DateTimeOffset dropoffUtcTime { get; init; }
	public string notes { get; init; }
}

public record Order
{
    public int ordernumber { get; init; }
    public DateTime date { get; init; }
    public string description { get; init; }
    public List<Load> loads { get; set; }
    public decimal total { get; set; }

}
