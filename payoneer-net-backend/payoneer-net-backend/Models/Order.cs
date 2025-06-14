using System.ComponentModel.DataAnnotations;

namespace payoneer_net_backend.Models;

public class Order
{
    [Key]
    public Guid OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}