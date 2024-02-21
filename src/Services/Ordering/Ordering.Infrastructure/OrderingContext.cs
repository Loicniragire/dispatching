namespace Ordering.Infrastructure;

/// <summary>
/// Provides the data access context for the Ordering domain, acting as an abstraction
/// over the underlying database and a central point for managing entity lifecycles
/// and domain events.
/// </summary>
public class OrderingContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "ordering";

	//
    // Properties for each entity set that corresponds to a database table.
	//
    public DbSet<Order> Orders { get; set; }
    public DbSet<Load> Loads { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    public DbSet<Client> Clients { get; set; }
	public DbSet<Delivery> Deliveries { get; set; }
	public DbSet<Address> Addresses { get; set; }

	// Mediator for dispatching domain events.
    private readonly IMediator _mediator;

	// Current transaction, if any.
    private IDbContextTransaction _currentTransaction;

    public OrderingContext(DbContextOptions<OrderingContext> options) : base(options) { }

    public OrderingContext(DbContextOptions<OrderingContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

	/// <summary>
	/// Indicates if there is an active transaction.
	/// </summary>
	/// <value>True if there is an active transaction, otherwise false.</value>
    public bool HasActiveTransaction => _currentTransaction != null;

	/// <summary>
	/// Applies entity configurations during the model creation phase.
	/// </summary>
	/// <param name="modelBuilder">The builder for the model to be created.</param>
	/// <remarks>
	/// This method is called during the model creation phase, allowing for the application
	/// of entity configurations to the model.
	/// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LoadEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
		modelBuilder.ApplyConfiguration(new DeliveryEntityTypeConfiguration());
    }

	/// <summary>
	/// Saves the entities with changes to the database and dispatches domain events.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token for the operation.</param>
	/// <returns>True if the operation succeeds, otherwise false.</returns>
	/// <remarks>
	/// This method saves the changes to the underlying database and dispatches any domain
	/// events that have been raised during the operation.
	/// </remarks>
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

	/// <summary>
	/// Initiates a new transaction if there isn't an active one.
	/// </summary>
	/// <returns>The newly created database transaction.</returns>
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

	/// <summary>
	/// Commits the provided transaction against the database.
	/// </summary>
	/// <param name="transaction">The transaction to commit.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

	/// <summary>
	/// Rolls back the current transaction, if any, and disposes it.
	/// </summary>
    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}


/// <summary>
/// Factory for creating design-time instances of OrderingContext for use with
/// Entity Framework migrations and tooling.
/// </summary>
/// <remarks>
/// This class provides a factory method for creating instances of the OrderingContext
/// for use with Entity Framework migrations and tooling. It is used to create a new
/// instance of the context with design-time configuration, such as a hard-coded
/// connection string, for use with the EF Core CLI tools.
/// </remarks>
public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<OrderingContext>
{
    public OrderingContext CreateDbContext(string[] args)
    {
		// Using a hard-coded connection string for design-time services.
        var optionsBuilder = new DbContextOptionsBuilder<OrderingContext>()
            .UseSqlServer("Server=localhost,1433;Database=dispatching.Services.OrderingDb;User ID=SA;Password=1Strong@Psw;TrustServerCertificate=True;Connection Timeout=30;");

        return new OrderingContext(optionsBuilder.Options, new NoMediator());
    }

	/// <summary>
	/// Provides a non-functional mediator implementation for design-time purposes.
	/// </summary>
    class NoMediator : IMediator
    {
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return default(IAsyncEnumerable<TResponse>);
        }

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            return default(IAsyncEnumerable<object?>);
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<TResponse>(default(TResponse));
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(object));
        }
    }
}
