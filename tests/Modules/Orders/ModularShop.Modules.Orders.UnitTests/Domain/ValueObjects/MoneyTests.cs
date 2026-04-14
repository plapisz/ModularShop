using ModularShop.Modules.Orders.Domain.Exceptions;
using ModularShop.Modules.Orders.Domain.ValueObjects;
using Shouldly;

namespace ModularShop.Modules.Orders.UnitTests.Domain.ValueObjects;

public sealed class MoneyTests
{
    [Fact]
    public void GivenConstructor_WhenValidData_ThenCreatesMoneyWithCorrectValues()
    {
        // Arrange
        const decimal amount = 100m;
        const string currency = "PLN";
        
        // Act
        var money = new Money(amount, currency);

        // Assert
        money.Amount.ShouldBe(100m);
        money.Currency.ShouldBe("PLN");
    }

    [Fact]
    public void GivenConstructor_WhenZeroAmount_ThenCreatesMoney()
    {
        // Arrange
        const decimal amount = 0m;
        const string currency = "PLN";
        
        // Act
        var exception = Record.Exception(() => new Money(amount, currency));

        // Assert
        exception.ShouldBeNull();
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void GivenConstructor_WhenNegativeAmount_ThenThrowsInvalidAmountException(decimal amount)
    {
        // Arrange
        const string currency = "PLN";
        
        // Act
        var exception = Record.Exception(() => new Money(amount, currency));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidAmountException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GivenConstructor_WhenNullOrWhiteSpaceCurrency_ThenThrowsInvalidCurrencyException(string? currency)
    {
        // Arrange
        const decimal amount = 100m;
        
        // Act
        var exception = Record.Exception(() => new Money(amount, currency!));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidCurrencyException>();
    }
    
    [Fact]
    public void GivenZeroMethod_WhenCurrency_ThenReturnsMoneyWithZeroAmountAndCorrectCurrency()
    {
        // Arrange
        const string currency = "PLN";
        
        // Act
        var money = Money.Zero(currency);

        // Assert
        money.Amount.ShouldBe(0m);
        money.Currency.ShouldBe("PLN");
    }
    
    [Theory]
    [InlineData(100, 2, 200)]
    [InlineData(99.99, 3, 299.97)]
    [InlineData(0, 5, 0)]
    public void GivenMultiply_WhenValidFactor_ThenReturnsCorrectAmount(decimal amount, int factor, decimal expected)
    {
        // Arrange
        var money = new Money(amount, "PLN");

        // Act
        var result = money.Multiply(factor);

        // Assert
        result.Amount.ShouldBe(expected);
    }

    [Fact]
    public void GivenMultiply_WhenValidFactor_ThenRetainsCurrency()
    {
        // Arrange
        const decimal amount = 200m;
        var money = new Money(amount, "PLN");

        // Act
        var result = money.Multiply(2);

        // Assert
        result.Currency.ShouldBe("PLN");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void GivenNonPositiveFactor_ThrowsInvalidMultiplierException(int factor)
    {
        // Arrange
        const decimal amount = 200m;
        var money = new Money(amount, "PLN");

        // Act
        var exception = Record.Exception(() => money.Multiply(factor));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidMultiplierException>();
    }
    
    [Fact]
    public void GivenSumMethod_WhenMoneyCollectionWithSameCurrency_ThenReturnsSummedAmount()
    {
        // Arrange
        var values = new[]
        {
            new Money(150m, "PLN"),
            new Money(200m, "PLN"),
            new Money(50m, "PLN"),
        };

        // Act
        var result = Money.Sum(values);

        // Assert
        result.Amount.ShouldBe(400m);
        result.Currency.ShouldBe("PLN");
    }

    [Fact]
    public void GivenSumMethod_WhenSingleElement_ThenReturnsThatElement()
    {
        // Arrange
        var values = new[] { new Money(200, "PLN")};

        // Act
        var result = Money.Sum(values);

        // Assert
        result.Amount.ShouldBe(200m);
    }

    [Fact]
    public void GivenSumMethod_WhenMixedCurrencies_ThenThrowsInvalidCurrencyException()
    {
        // Arrange
        var values = new[]
        {
            new Money(200m, "PLN"),
            new Money(200m, "USD"),
        };

        // Act
        var exception = Record.Exception(() => Money.Sum(values));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidCurrencyException>();
    }

    [Fact]
    public void GivenSumMethod_WhenNullCollection_ThrowsEmptyMoneyCollectionException()
    {
        // Arrange
        Money[]? values = null;
        
        // Act
        var exception = Record.Exception(() => Money.Sum(values!));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyMoneyCollectionException>();
    }

    [Fact]
    public void GivenSumMethod_WhenEmptyCollection_ThrowsEmptyMoneyCollectionException()
    {
        // Arrange
        var values = Array.Empty<Money>();
        
        // Act
        var exception = Record.Exception(() => Money.Sum(values));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyMoneyCollectionException>();
    }
    
    [Theory]
    [InlineData(100, "PLN", "100.00 PLN")]
    [InlineData(99.9, "USD", "99.90 USD")]
    [InlineData(0, "EUR", "0.00 EUR")]
    public void GivenToStringMethod_WhenMoneyCreated_ThenReturnsFormattedString(decimal amount, string currency, string expected)
    {
        // Arrange
        var money = new Money(amount, currency);

        // Act
        var result = money.ToString();
        
        // Assert
        result.ShouldBe(expected);
    }
    
    [Fact]
    public void GivenEqualityBehavior_WhenSameAmountAndCurrency_ThenAreEqual()
    {
        // Arrange
        var a = new Money(200m, "PLN");
        var b = new Money(200m, "PLN");

        // Assert
        var result = a == b;
        
        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenEqualityBehavior_WhenDifferentAmount_ThenAreNotEqual()
    {
        // Arrange
        var a = new Money(300m, "PLN");
        var b = new Money(200m, "PLN");

        // Assert
        var result = a == b;
        
        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenEqualityBehavior_WhenDifferentCurrency_ThenAreNotEqual()
    {
        // Arrange
        var a = new Money(200m, "PLN");
        var b = new Money(200m, "USD");

        // Assert
        var result = a == b;
        
        // Assert
        result.ShouldBeFalse();
    }
}