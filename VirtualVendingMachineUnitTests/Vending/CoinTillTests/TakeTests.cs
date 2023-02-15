using System;
using System.Linq;
using FluentAssertions;
using VirtualVendingMachine.Exceptions;
using VirtualVendingMachine.Extensions;
using VirtualVendingMachine.Tills;
using VirtualVendingMachineUnitTests.Builders;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.CoinTillTests;

public class TakeTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(40)]
    [InlineData(50)]
    [InlineData(60)]
    [InlineData(90)]
    [InlineData(160)]
    public void ReturnsExpectedTotalAmountOfCoins(int amountToTake)
    {
        // Arrange
        var till = CoinTillBuilder.BuildWithStockedCoinBank();

        // Act
        var actual = till.Take(amountToTake);

        // Assert
        actual.Sum().Should().Be(amountToTake);
    }

    [Fact]
    public void RemovesCoinFromTillAfterTaking()
    {
        // Arrange
        var till = CoinTillBuilder.Build(new[] { Coin.Create(50) });

        // Act
        till.Take(50);

        // Assert
        till.CountCoinsFor(50).Should().Be(0);
    }

    [Fact]
    public void ThrowsIfAttemptingToTakeNegativeAmount()
    {
        // Arrange
        var till = CoinTillBuilder.Build();

        // Act
        var actual = () => till.Take(-1);

        // Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void ThrowsIfAttemptingToTakeLessThanSmallestSupportedCoin()
    {
        // Arrange
        var till = CoinTillBuilder.Build();
        var tinyCoin = TestConstants.CoinTill.AcceptedCoinValues.Min() / 2;

        // Act
        var actual = () => till.Take(tinyCoin);

        // Assert
        actual.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void ThrowsExceptionIfNotEnoughCoins()
    {
        // Arrange
        var till = CoinTillBuilder.Build(new[] { Coin.Create(10) });

        // Act
        var actual = () => till.Take(50);

        // Assert
        actual.Should().ThrowExactly<InsufficientFundsInChangeBankException>();
    }
}