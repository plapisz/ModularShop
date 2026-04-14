using ModularShop.Modules.Orders.Domain.Exceptions;
using ModularShop.Modules.Orders.Domain.ValueObjects;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Domain.ValueObjects;

public sealed class OrderNumberTests
{ 
    [Fact] 
    public void GivenNewMethod_WhenCalled_ThenReturnsOrderNumberWithOrdPrefix() 
    { 
        // Act
        var orderNumber = OrderNumber.New();
        
        // Assert
        orderNumber.Value.ShouldStartWith("ORD-");
    }
    
    [Fact] 
    public void GivenNewMethod_WhenCalledTwice_ThenReturnsDifferentValues() 
    { 
        // Arrange
        var first = OrderNumber.New(); 
        var second = OrderNumber.New();
        
        // Act
        var result = first == second;
        
        // Assert
        result.ShouldBeFalse();
    }
    
    [Fact] 
    public void GivenNewMethod_WhenCalled_ThenReturnsUpperCaseValue() 
    { 
        // Act
        var orderNumber = OrderNumber.New();
        
        // Assert
        orderNumber.Value.ShouldBe(orderNumber.Value.ToUpperInvariant()); 
    }
    
    [Fact] 
    public void GivenFromMethod_WhenValidValue_ThenCreatesOrderNumberWithThatValue() 
    {
        // Arrange
        const string number = "ORD-ABC123";
        
        // Act
        var orderNumber = OrderNumber.From(number);

        // Assert
        orderNumber.Value.ShouldBe(number);
    }
    
    [Theory] 
    [InlineData(null)] 
    [InlineData("")] 
    [InlineData("   ")] 
    public void GivenFromMethod_WhenNullOrWhiteSpaceValue_ThenThrowsInvalidOrderNumberException(string? value) 
    {
        // Act
        var exception = Record.Exception(() => OrderNumber.From(value!));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidOrderNumberException>(); 
    }
    
    [Fact] 
    public void GivenToStringMethod_WhenCalled_ThenReturnsValue() 
    { 
        // Arrange
        var orderNumber = OrderNumber.From("ORD-ABC123");

        // Act
        var result = orderNumber.ToString();
        
        // Assert
        result.ShouldBe("ORD-ABC123");
    }
    
    [Fact] 
    public void GivenEqualityBehavior_WhenSameValue_ThenAreEqual() 
    { 
        // Arrange
        var a = OrderNumber.From("ORD-ABC123");
        var b = OrderNumber.From("ORD-ABC123");

        // Act
        var result = a == b;

        // Assert
        result.ShouldBeTrue();
    }
    
    [Fact] 
    public void GivenEqualityBehavior_WhenDifferentValue_ThenAreNotEqual() 
    { 
        // Arrange
        var a = OrderNumber.From("ORD-ABC123"); 
        var b = OrderNumber.From("ORD-XYZ789");

        // Act
        var result = a == b;
        
        // Assert
        result.ShouldBeFalse();
    }
}