using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using payoneer_net_backend.DbContexts;
using payoneer_net_backend.Interfaces;
using payoneer_net_backend.Models;

namespace payoneer_net_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderRepository repository, ILogger<OrdersController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating a new order with ID: {OrderId}", request.OrderId);
        var order = new Order
        {
            OrderId = request.OrderId,
            CustomerName = request.CustomerName,
            CreatedAt = request.CreatedAt,
            Items = request.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };

        var createdOrder = await _repository.CreateOrderAsync(order);
        _logger.LogInformation("Successfully created order with ID: {OrderId}", createdOrder.OrderId);

        return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.OrderId }, new { createdOrder.OrderId });
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        _logger.LogInformation("Getting all orders");
        var orders = await _repository.GetAllOrdersAsync();
        var orderDtos = orders.Select(o => new OrderDto // Project to DTO
        {
            OrderId = o.OrderId,
            CustomerName = o.CustomerName,
            CreatedAt = o.CreatedAt,
            Items = o.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        });

        return Ok(orderDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
    {
        _logger.LogInformation("Getting order with ID: {OrderId}", id);
        var order = await _repository.GetOrderByIdAsync(id);

        if (order == null)
        {
            _logger.LogWarning("Order with ID: {OrderId} not found.", id);
            return NotFound();
        }
        
        var orderDto = new OrderDto()
        {
            OrderId = order.OrderId,
            CustomerName = order.CustomerName,
            CreatedAt = order.CreatedAt,
            Items = order.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };

        return Ok(orderDto);
    }
}