using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using payoneer_net_backend;
using payoneer_net_backend.DbContexts;
using payoneer_net_backend.Models;
using payoneer_net_backend.Interfaces;
using payoneer_net_backend.Repositories;

namespace IntegrationTests;

public class OrderControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly string _dbName = $"TestDb_{Guid.NewGuid()}";

    public OrderControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<OrderDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                
                services.AddDbContext<OrderDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_dbName);
                });
                
                services.AddScoped<IOrderRepository, OrderRepository>();
                
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
                context.Database.EnsureCreated();
            });
        }).CreateClient();
    }

    [Fact]
    public async Task PostOrder_ThenGetOrder_ReturnsCorrectOrder()
    {
        var createRequest = new CreateOrderRequest
        {
            OrderId = Guid.NewGuid(),
            CustomerName = "Integration Test Customer",
            CreatedAt = DateTime.UtcNow,
            Items = new() { new OrderItemDto { ProductId = Guid.NewGuid(), Quantity = 1 } }
        };
        
        var createResponse = await _client.PostAsJsonAsync("/api/orders", createRequest);
        if (createResponse.StatusCode != HttpStatusCode.Created)
        {
            var errorContent = await createResponse.Content.ReadAsStringAsync();
            Console.WriteLine("POST /api/orders failed: " + errorContent);
        }

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        
        var getResponse = await _client.GetAsync($"/api/orders/{createRequest.OrderId}");
        
        getResponse.EnsureSuccessStatusCode(); 
        var retrievedOrder = await getResponse.Content.ReadFromJsonAsync<OrderDto>();

        Assert.NotNull(retrievedOrder);
        Assert.Equal(createRequest.OrderId, retrievedOrder.OrderId);
        Assert.Equal("Integration Test Customer", retrievedOrder.CustomerName);
    }
} 