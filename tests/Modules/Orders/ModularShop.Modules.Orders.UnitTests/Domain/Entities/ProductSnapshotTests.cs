using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.Exceptions;
using ModularShop.Modules.Orders.Domain.ValueObjects;
using ModularShop.Modules.Orders.UnitTests.Common.Builders;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Domain.Entities;

public sealed class ProductSnapshotTests
{
    [Fact]
    public void GivenConstructor_WhenValidData_ThenCreatesProductSnapshotWithCorrectValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var unitPrice = new Money(100m, "PLN");
        const string name = "Test Product";

        // Act
        var snapshot = new ProductSnapshot(id, name, unitPrice);

        snapshot.Id.ShouldBe(id);
        snapshot.Name.ShouldBe(name);
        snapshot.UnitPrice.ShouldBe(unitPrice);
        snapshot.IsActive.ShouldBeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GivenConstructor_WhenNullOrWhiteSpaceName_ThenThrowsInvalidProductNameException(string? name)
    {
        // Arrange
        var id = Guid.NewGuid();
        var unitPrice = new Money(100m, "PLN");

        // Act
        var exception = Record.Exception(() => new ProductSnapshot(id, name!, unitPrice));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidProductNameException>();
    }
    
    [Fact]
    public void GivenUpdate_WhenValidData_ThenUpdatesNameAndUnitPrice()
    {
        // Arrange
        var snapshot = new ProductSnapshotBuilder()
            .WithName("Old Name")
            .WithUnitPrice(new Money(100m, "PLN"))
            .Build();

        // Act
        snapshot.Update("New Name", new Money(200m, "PLN"));

        // Assert
        snapshot.Name.ShouldBe("New Name");
        snapshot.UnitPrice.ShouldBe(new Money(200m, "PLN"));
    }

    [Fact]
    public void GivenUpdate_WhenValidData_ThenDoesNotChangeId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var snapshot = new ProductSnapshotBuilder()
            .WithId(id)
            .Build();

        // Act
        snapshot.Update("New Name", new Money(200m, "PLN"));

        // Assert
        snapshot.Id.ShouldBe(id);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GivenUpdate_WhenNullOrWhiteSpaceName_ThenThrowsInvalidProductNameException(string? name)
    {
        // Arrange
        var snapshot = new ProductSnapshotBuilder().Build();

        // Act
        var exception = Record.Exception(() => snapshot.Update(name!, new Money(100m, "PLN")));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidProductNameException>();
    }
    
    [Fact]
    public void GivenDeactivate_WhenCalled_ThenSetsIsActiveToFalse()
    {
        // Arrange
        var snapshot = new ProductSnapshotBuilder().Build();

        // Act
        snapshot.Deactivate();

        // Assert
        snapshot.IsActive.ShouldBeFalse();
    }
}