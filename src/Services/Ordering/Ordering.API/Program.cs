
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Get connection string
string constr = builder.Configuration.GetConnectionString("DefaultConnection");

// Register OrderingContext for DI
builder.Services.AddDbContext<OrderingContext>(options =>
{
	options.UseSqlServer(constr);
});
//
// Add services to the DI container
// add scoped OrderingQueries with connection string
builder.Services.AddScoped<IOrderQueries>(sp => new OrderQueries(constr));
builder.Services.AddScoped<IRequestManager, RequestManager>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining(typeof(CreateOrderCommandHandler));
});
builder.Services.AddLogging(); // This adds a default ILogger implementation


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Ordering.API", Version = "v1" });
        });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));
}

// Register the exception handling middleware. This must be registered before any other middleware that might throw an exception
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Register the request logging middleware
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
