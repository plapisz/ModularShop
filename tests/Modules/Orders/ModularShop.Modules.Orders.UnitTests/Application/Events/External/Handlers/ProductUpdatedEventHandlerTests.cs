using ModularShop.Modules.Orders.Application.Events.External;
using ModularShop.Modules.Orders.Application.Events.External.Handlers;
using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Modules.Orders.Domain.ValueObjects;
using ModularShop.Modules.Orders.UnitTests.Common.Builders;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Application.Events.External.Handlers;

public sealed class ProductUpdatedEventHandlerTests
{
    private readonly IProductSnapshotRepository _productSnapshotRepository;
    private readonly ProductUpdatedEventHandler _handler;

    public ProductUpdatedEventHandlerTests()
    {
        _productSnapshotRepository = Substitute.For<IProductSnapshotRepository>();
        _handler = new ProductUpdatedEventHandler(_productSnapshotRepository);
    }

    [Fact]
    public async Task GivenHandleAsync_WhenProductSnapshotNotFound_ThenDoesNotUpdateRepository()
    {
        // Arrange
        var @event = new ProductUpdatedEvent(Guid.NewGuid(), "New Name", 200m);

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
    public async Task GivenHandleAsync_WhenProductSnapshotFound_ThenUpdatesNameAndPrice()
    {
        // Arrange
        var @event = new ProductUpdatedEvent(Guid.NewGuid(), "New Name", 200m);
        
        var productSnapshot = new ProductSnapshotBuilder()
            .WithName("Old Name")
            .WithUnitPrice(new Money(100m, "PLN"))
            .Build();

        _productSnapshotRepository
            .GetAsync(@event.Id, Arg.Any<CancellationToken>())
            .Returns(productSnapshot);

        // Act
        await _handler.HandleAsync(@event, CancellationToken.None);

        // Assert
        productSnapshot.Name.ShouldBe(@event.Name);
        productSnapshot.UnitPrice.ShouldBe(new Money(@event.Price, "PLN"));

        await _productSnapshotRepository
            .Received(1)
            .UpdateAsync(productSnapshot, Arg.Any<CancellationToken>());
    }
}