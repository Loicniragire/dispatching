using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Application.Queries
{
	public interface IOrderQueries
	{
		Task<Order> GetOrderAsync(int id);
		Task<IEnumerable<Order>> GetOrdersAsync();
	}
}
