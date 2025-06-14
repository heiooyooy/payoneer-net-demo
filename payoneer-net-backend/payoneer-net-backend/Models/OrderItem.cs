using System.ComponentModel.DataAnnotations;

namespace payoneer_net_backend.Models;

public class OrderItem
{
    [Key]
    public int Id { get; set; } 
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; } 
    public Order Order { get; set; } = null!;
}