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
        var result = await connection.QueryAsync<Order>("SELECT * FROM Orders WHERE Id = @id", new { id });

        if (result == null || result.Count() == 0)
        {
            throw new KeyNotFoundException();
        }

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        return await connection.QueryAsync<Order>("SELECT * FROM Orders");
    }
}
