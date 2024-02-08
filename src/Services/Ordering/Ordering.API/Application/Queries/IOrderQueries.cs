using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Queries
{
	/// <summary>
	/// Order queries interface.
	/// Optimized for CQRS pattern - Read operations.
	/// Should return view models rather than domain models defined tin Damain.AggregatesModel.
	/// </summary>
	public interface IOrderQueries
	{
		Task<Order> GetOrderAsync(int id);
		Task<IEnumerable<Order>> GetOrdersAsync();
	}
}
