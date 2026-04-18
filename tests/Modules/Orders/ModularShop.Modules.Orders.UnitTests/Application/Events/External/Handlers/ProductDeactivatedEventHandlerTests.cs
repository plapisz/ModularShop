using ModularShop.Modules.Orders.Application.Events.External;
using ModularShop.Modules.Orders.Application.Events.External.Handlers;
using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Modules.Orders.UnitTests.Common.Builders;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Application.Events.External.Handlers;

public sealed class ProductDeactivatedEventHandlerTests
{
    private readonly IProductSnapshotRepository _productSnapshotRepository;
    private readonly ProductDeactivatedEventHandler _handler;

    public ProductDeactivatedEventHandlerTests()
    {
        _productSnapshotRepository = Substitute.For<IProductSnapshotRepository>();
        _handler = new ProductDeactivatedEventHandler(_productSnapshotRepository);
    }

    [Fact]
    public async Task GivenHandleAsync_WhenProductSnapshotNotFound_ThenDoesNotUpdateRepository()
    {
        // Arrange
        var @event = new ProductDeactivatedEvent(Guid.NewGuid());
        
        _productSnapshotRepository
            .GetAsync(@event.Id, Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        await _handler.HandleAsync(@event, CancellationToken.None);

        // Assert
        await _productSnapshotRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<ProductSnapshot>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GivenHandleAsync_WhenProductSnapshotFound_ThenDeactivatesProductSnapshot()
    {
        // Arrange
        var @event = new ProductDeactivatedEvent(Guid.NewGuid());
        
        var productSnapshot = new ProductSnapshotBuilder().Build();

        _productSnapshotRepository
            .GetAsync(@event.Id, Arg.Any<CancellationToken>())
            .Returns(productSnapshot);

        // Act
        await _handler.HandleAsync(@event, CancellationToken.None);

        // Assert
        productSnapshot.IsActive.ShouldBeFalse();
        
        await _productSnapshotRepository
            .Received(1)
            .UpdateAsync(productSnapshot, Arg.Any<CancellationToken>());
    }
}