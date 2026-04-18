using ModularShop.Modules.Orders.Application.Events.External;
using ModularShop.Modules.Orders.Application.Events.External.Handlers;
using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Modules.Orders.Domain.ValueObjects;
using NSubstitute;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Application.Events.External.Handlers;

public sealed class ProductCreatedEventHandlerTests
{
    private readonly IProductSnapshotRepository _productSnapshotRepository;
    private readonly ProductCreatedEventHandler _handler;

    public ProductCreatedEventHandlerTests()
    {
        _productSnapshotRepository = Substitute.For<IProductSnapshotRepository>();
        _handler = new ProductCreatedEventHandler(_productSnapshotRepository);
    }

    [Fact]
    public async Task GivenHandleAsync_WhenCalled_ThenCreatesProductSnapshot()
    {
        // Arrange
        var @event = new ProductCreatedEvent(Guid.NewGuid(), "Test Product", 100m);
        
        ProductSnapshot? capturedSnapshot = null;

        await _productSnapshotRepository
            .AddAsync(
                Arg.Do<ProductSnapshot>(p => capturedSnapshot = p),
                Arg.Any<CancellationToken>());

        // Act
        await _handler.HandleAsync(@event, CancellationToken.None);

        // Assert
        capturedSnapshot.ShouldNotBeNull();
        capturedSnapshot.Id.ShouldBe(@event.Id);
        capturedSnapshot.Name.ShouldBe(@event.Name);
        capturedSnapshot.UnitPrice.ShouldBe(new Money(@event.Price, "PLN"));
        capturedSnapshot.IsActive.ShouldBeTrue();
        
        await _productSnapshotRepository
            .Received(1)
            .AddAsync(capturedSnapshot, Arg.Any<CancellationToken>());
    }
}