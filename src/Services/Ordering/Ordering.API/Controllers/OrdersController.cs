using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Application.Queries;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderQueries _orderQueries;
        private readonly IMediator _mediator;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderQueries orderQueries, IMediator mediator, ILogger<OrdersController> logger)
        {
            _orderQueries = orderQueries ?? throw new ArgumentNullException(nameof(orderQueries));
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Retrieves all orders
        [HttpGet] // Responds to GET request at the route 'api/v1/orders'
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersAsync()
        {
            try
            {
                var orders = await _orderQueries.GetOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetOrdersAsync failed.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // Retrieves a single order by ID
        [HttpGet("{id:int}")] // Responds to GET request at the route 'api/v1/orders/{id}'
        public async Task<ActionResult<Order>> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _orderQueries.GetOrderAsync(id);
                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }
                
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetOrderByIdAsync failed.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
