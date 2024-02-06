using Ordering.API.Application.Commands;
using Ordering.API.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add services to the DI container
builder.Services.AddScoped<IOrderQueries, OrderQueries>(); 
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssemblyContaining(typeof(CreateOrderCommandHandler));
});
builder.Services.AddLogging(); // This adds a default ILogger implementation


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

