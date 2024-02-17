using Microsoft.EntityFrameworkCore;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.SeedWork;

namespace Ordering.Infrastructure;

public class OrderingContext : DbContext, IUnitOfWork
{
	public const string DEFAULT_SCHEMA = "ordering";
	public DbSet<Order> Orders { get; set; }
	public DbSet<Load> Loads { get; set; }
	public DbSet<OrderStatus> OrderStatus { get; set; }
	public DbSet<Customer> Clients { get; set; }


    public OrderingContext(DbContextOptions<OrderingContext> options) : base(options)
    {
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

