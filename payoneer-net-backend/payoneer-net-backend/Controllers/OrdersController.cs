using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using payoneer_net_backend.DbContexts;
using payoneer_net_backend.Models;

namespace payoneer_net_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly OrderDbContext _context;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(OrderDbContext context, ILogger<OrdersController> logger)
    {
        _context = context;
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

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, new { order.OrderId });
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        _logger.LogInformation("Getting all orders");
        var orders = await _context.Orders
            .Include(o => o.Items) 
            .Select(o => new OrderDto // Project to DTO
            {
                OrderId = o.OrderId,
                CustomerName = o.CustomerName,
                CreatedAt = o.CreatedAt,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            })
            .ToListAsync();

        return Ok(orders);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
    {
        _logger.LogInformation("Getting order with ID: {OrderId}", id);
        var order = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.OrderId == id)
            .Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                CustomerName = o.CustomerName,
                CreatedAt = o.CreatedAt,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }
}