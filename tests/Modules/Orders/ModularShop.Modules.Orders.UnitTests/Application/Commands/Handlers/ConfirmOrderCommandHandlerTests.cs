using ModularShop.Modules.Orders.Application.Commands;
using ModularShop.Modules.Orders.Application.Commands.Handlers;
using ModularShop.Modules.Orders.Application.Exceptions;
using ModularShop.Modules.Orders.Domain.Aggregates;
using ModularShop.Modules.Orders.Domain.Enums;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Modules.Orders.UnitTests.Common.Builders;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Application.Commands.Handlers;

public sealed class ConfirmOrderCommandHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly ConfirmOrderCommandHandler _handler;

    public ConfirmOrderCommandHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _handler = new ConfirmOrderCommandHandler(_orderRepository);
    }

    [Fact]
    public async Task GivenHandleAsync_WhenOrderNotFound_ThenThrowsOrderNotFoundException()
    {
        // Arrange
        var command = new ConfirmOrderCommand(Guid.NewGuid());

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
    public async Task GivenHandleAsync_WhenOrderFound_ThenConfirmsOrder()
    {
        var command = new ConfirmOrderCommand(Guid.NewGuid());
        var order = new OrderBuilder().WithItem().Build();

        _orderRepository.GetAsync(command.OrderId, Arg.Any<CancellationToken>())
            .Returns(order);

        await _handler.HandleAsync(command, CancellationToken.None);

        order.Status.ShouldBe(OrderStatus.Confirmed);
   
        await _orderRepository
            .Received(1)
            .UpdateAsync(order, Arg.Any<CancellationToken>());
    }
}