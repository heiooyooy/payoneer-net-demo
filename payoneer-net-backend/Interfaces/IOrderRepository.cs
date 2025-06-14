using payoneer_net_backend.Models;

namespace payoneer_net_backend.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(Guid id);
    Task<Order> CreateOrderAsync(Order order);
}