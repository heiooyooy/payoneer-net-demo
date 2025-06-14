namespace payoneer_net_backend.Models;

public class CreateOrderRequest
{
    public Guid OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItemDto> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}