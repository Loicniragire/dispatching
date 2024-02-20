using MediatR;
using Ordering.API.Application.Commands;
using Ordering.API.Application.Queries;
using Ordering.Infrastructure.Idempotency;
using Ordering.Infrastructure.Repositories;
using Ordering.Domain.AggregatesModel.OrderAggregate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add services to the DI container
builder.Services.AddScoped<IOrderQueries, OrderQueries>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

