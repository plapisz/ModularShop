using ModularShop.Modules.Orders.Application.Commands;
using ModularShop.Modules.Orders.Application.Commands.Handlers;
using ModularShop.Modules.Orders.Application.Exceptions;
using ModularShop.Modules.Orders.Domain.Aggregates;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Modules.Orders.Domain.ValueObjects;
using ModularShop.Modules.Orders.UnitTests.Common.Builders;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Application.Commands.Handlers;

public sealed class AddOrderItemCommandHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductSnapshotRepository _productRepository;
    private readonly AddOrderItemCommandHandler _handler;

    public AddOrderItemCommandHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _productRepository = Substitute.For<IProductSnapshotRepository>();
        
        _handler = new AddOrderItemCommandHandler(_orderRepository, _productRepository);
    }

    [Fact]
    public async Task GivenHandleAsync_WhenOrderNotFound_ThenThrowsOrderNotFoundException()
    {
        // Arrange
        var command = new AddOrderItemCommand(Guid.NewGuid(), Guid.NewGuid(), 1);

        _orderRepository
            .GetAsync(command.OrderId, Arg.Any<CancellationToken>())
            .ReturnsNull();
        
        // Act
        var exception = await Should.ThrowAsync<OrderNotFoundException>(() =>
            _handler.HandleAsync(command, CancellationToken.None));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<OrderNotFoundException>();
        
        await _orderRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GivenHandleAsync_WhenProductNotFound_ThenThrowsProductNotFoundException()
    {
        // Arrange
        var command = new AddOrderItemCommand(Guid.NewGuid(), Guid.NewGuid(), 1);
        
        var order = new OrderBuilder().Build();

        _orderRepository
            .GetAsync(command.OrderId, Arg.Any<CancellationToken>())
            .Returns(order);
        
        _productRepository
            .GetAsync(command.ProductId, Arg.Any<CancellationToken>())
            .ReturnsNull();
        
        // Act
        var exception =await Record.ExceptionAsync(async () 
            => await _handler.HandleAsync(command, CancellationToken.None));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProductNotFoundException>();
        
        await _orderRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GivenHandleAsync_WhenOrderAndProductFound_ThenAddsItemToOrder()
    {
        // Arrange
        var command = new AddOrderItemCommand(Guid.NewGuid(), Guid.NewGuid(), 2);
        
        var order = new OrderBuilder().Build();
        var product = new ProductSnapshotBuilder()
            .WithId(command.ProductId)
            .WithName("Test Product")
            .WithUnitPrice(new Money(100m, "PLN"))
            .Build();

        _orderRepository
            .GetAsync(command.OrderId, Arg.Any<CancellationToken>())
            .Returns(order);
        
        _productRepository
            .GetAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(product);

        // Act
        await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        order.Items.ShouldHaveSingleItem();
        var item = order.Items.Single();
        item.ProductId.ShouldBe(product.Id);
        item.ProductName.ShouldBe(product.Name);
        item.UnitPrice.ShouldBe(product.UnitPrice);
        item.Quantity.ShouldBe(command.Quantity);
        
        await _orderRepository
            .Received(1)
            .UpdateAsync(order, Arg.Any<CancellationToken>());
    }
}