using ModularShop.Modules.Orders.Application.Commands;
using ModularShop.Modules.Orders.Application.Commands.Handlers;
using ModularShop.Modules.Orders.Domain.Aggregates;
using ModularShop.Modules.Orders.Domain.Enums;
using ModularShop.Modules.Orders.Domain.Repositories;
using NSubstitute;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Application.Commands.Handlers;

public sealed class CreateOrderCommandHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _handler = new CreateOrderCommandHandler(_orderRepository);
    }

    [Fact]
    public async Task GivenHandleAsync_WhenCalled_ThenCreatesOrder()
    {
        // Arrange
        var command = new CreateOrderCommand(Guid.NewGuid(), Guid.NewGuid());
        Order? capturedOrder = null;

        await _orderRepository.AddAsync(
            Arg.Do<Order>(o => capturedOrder = o), 
            Arg.Any<CancellationToken>());

        // Act
        await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        capturedOrder.ShouldNotBeNull();
        capturedOrder.Status.ShouldBe(OrderStatus.Draft);
        capturedOrder.ShouldNotBeNull();
        capturedOrder.Items.ShouldBeEmpty();
        
        await _orderRepository
            .Received(1)
            .AddAsync( 
                Arg.Is<Order>(o => o.Id == command.OrderId && o.CustomerId == command.CustomerId), 
                Arg.Any<CancellationToken>());
    }
}