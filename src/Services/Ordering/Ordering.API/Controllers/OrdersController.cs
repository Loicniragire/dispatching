using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController: ControllerBase
    {
		private readonly IMediator _mediator;
		private readonly ILogger<OrdersController> _logger;

		public OrdersController(IMediator mediator, ILogger<OrdersController> logger)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

    }
}
