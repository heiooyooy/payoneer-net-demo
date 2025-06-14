using Microsoft.EntityFrameworkCore;
using payoneer_net_backend.Models;

namespace payoneer_net_backend.DbContexts;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId);
        
        var seedOrderId = new Guid("a5c2cb45-0217-4057-acf7-2da1d500a396");
        var seedProductId1 = new Guid("d3ffbd6f-4fc8-412c-bcd9-e3a4b4b25632");
        var seedProductId2 = new Guid("58340ce8-ed4e-4519-ab56-98c72f8e7524");
        
        modelBuilder.Entity<Order>().HasData(new Order
        {
            OrderId = seedOrderId,
            CustomerName = "Seed Customer",
            CreatedAt = new DateTime(2025, 6, 14, 0, 0, 0, DateTimeKind.Utc)
        });
        
        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem
            {
                Id = 1,
                OrderId = seedOrderId,
                ProductId = seedProductId1,
                Quantity = 10
            },
            new OrderItem
            {
                Id = 2,
                OrderId = seedOrderId,
                ProductId = seedProductId2,
                Quantity = 20
            }
        );
    }
}