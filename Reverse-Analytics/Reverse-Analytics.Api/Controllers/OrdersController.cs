using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Order;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly ILogger<OrdersController> _logger;

        const int pageSize = 15;

        public OrdersController(IOrderService service, ILogger<OrdersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersAsync(int pageSize = pageSize, int pageNumber = 1)
        {
            try
            {
                var orders = await _service.GetAllOrdersAsync(pageSize, pageNumber);

                if (orders is null || orders.Count() < 1)
                {
                    return Ok("There is no Orders.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while retrieving Orders.", ex.Message);
                return StatusCode(500, "There was an error retrieving Order. Please, try agian later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _service.GetOrderByIdAsync(id);

                if (order is null)
                {
                    return NotFound($"There is no Order with id: {id}.");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Order with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Order with id: {id}. Please, try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrderAsync([FromBody] OrderForCreate orderToCreate)
        {
            try
            {
                if (orderToCreate is null)
                {
                    return BadRequest("Order to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Order to create is not valid.");
                }

                var createdOrder = await _service.CreateOrderAsync(orderToCreate);

                if (orderToCreate is null)
                {
                    return StatusCode(500, "Something went wrong while creating new Order. Please, try again.");
                }

                return Ok(createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating new Order.", ex.Message);
                return StatusCode(500, $"There was an error creating new Order. Please, try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderAsync([FromBody] OrderForUpdate orderToUpdate, int orderId)
        {
            try
            {
                if (orderToUpdate is null)
                {
                    return BadRequest("Order to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Order to update is not valid.");
                }

                if (orderToUpdate.Id != orderId)
                {
                    return BadRequest($"Route id: {orderToUpdate.Id} does not match with route id: {orderId}.");
                }

                await _service.UpdateOrderAsync(orderToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating Order with id: {orderId}.", ex.Message);
                return StatusCode(500, $"There was an error updating Order with id: {orderId}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderAsync(int id)
        {
            try
            {
                await _service.DeleteOrderAsync(id);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while deleting Order with id: {id}.", ex.Message);
                return StatusCode(500, "There was an error deleting Order with id: {id}. Please, try again later.");
            }
        }
    }
}
