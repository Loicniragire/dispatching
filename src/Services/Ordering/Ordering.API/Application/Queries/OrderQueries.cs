using System.Data.SqlClient;
using Dapper;

namespace Ordering.API.Application.Queries;

public class OrderQueries : IOrderQueries
{
    private readonly string _connectionString;

    public OrderQueries(string constr)
    {
        _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var result = await connection.QueryAsync<dynamic>("SELECT * FROM Orders WHERE Id = @id", new { id });

        if (result == null || result.Count() == 0)
        {
            throw new KeyNotFoundException();
        }

        return MapOrderItems(result);
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        return await connection.QueryAsync<Order>("SELECT * FROM Orders");
    }

    private Order MapOrderItems(dynamic result)
    {
        var order = new Order
        {
            ordernumber = result[0].Id,
            date = result[0].Date,
            description = result[0].Description,
            loads = new List<Load>(),
            total = result[0].Total,
        };

        foreach (var item in result)
        {
            order.loads.Add(new Load
            {
                productname = item.ProductName,
                units = item.Units,
                unitprice = item.UnitPrice,
                pickup = new Address
                {
                    street = item.PickupStreet,
                    city = item.PickupCity,
                    state = item.PickupState,
                    zip = item.PickupZip
                },
                dropoff = new Address
                {
                    street = item.DropoffStreet,
                    city = item.DropoffCity,
                    state = item.DropoffState,
                    zip = item.DropoffZip
                },
                status = (LoadStatus)Enum.Parse(typeof(LoadStatus), item.Status),
                description = item.Description,
                pickupUtcTime = item.PickupUtcTime,
                dropoffUtcTime = item.DropoffUtcTime,
                notes = item.Notes
            });
        }

        return order;
    }
}
