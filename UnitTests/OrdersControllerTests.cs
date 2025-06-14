using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using payoneer_net_backend.Controllers;
using payoneer_net_backend.Interfaces;
using payoneer_net_backend.Models;

namespace UnitTests;

public class OrdersControllerTests
{
    private readonly Mock<IOrderRepository> _mockRepo;
    private readonly Mock<ILogger<OrdersController>> _mockLogger;
    private readonly OrdersController _controller;
    private readonly Fixture _fixture;

    public OrdersControllerTests()
    {
        _mockRepo = new Mock<IOrderRepository>();
        _mockLogger = new Mock<ILogger<OrdersController>>();
        _controller = new OrdersController(_mockRepo.Object, _mockLogger.Object);
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task GetOrder_WithExistingId_ReturnsOkResult()
    {
        var order = _fixture.Create<Order>();
        _mockRepo.Setup(repo => repo.GetOrderByIdAsync(order.OrderId))
            .ReturnsAsync(order);

        var result = await _controller.GetOrder(order.OrderId);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetOrder_WithNonExistingId_ReturnsNotFoundResult()
    {
        var nonExistentId = Guid.NewGuid();
        _mockRepo.Setup(repo => repo.GetOrderByIdAsync(nonExistentId))
            .ReturnsAsync((Order?)null);
        
        var result = await _controller.GetOrder(nonExistentId);
        
        Assert.IsType<NotFoundResult>(result.Result);
    }
}