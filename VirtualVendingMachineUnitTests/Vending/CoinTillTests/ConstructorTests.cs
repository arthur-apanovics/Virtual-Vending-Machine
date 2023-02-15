using System;
using System.Linq;
using FluentAssertions;
using VirtualVendingMachine.Tills;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.CoinTillTests;

public class ConstructorTests
{
    [Fact]
    public void HoldsProvidedChangeBank()
    {
        // Arrange
        const int expectedTenCentCoins = 50;
        const int expectedTwentyCentCoins = 25;
        const int expectedFiftyCentCoins = 15;
        const int expectedOneDollarCoins = 10;
        const int expectedTwoDollarCoins = 5;

        var coins = Array.Empty<Coin>()
            .Concat(Enumerable.Repeat(Coin.Create(10), expectedTenCentCoins))
            .Concat(Enumerable.Repeat(Coin.Create(20), expectedTwentyCentCoins))
            .Concat(Enumerable.Repeat(Coin.Create(50), expectedFiftyCentCoins))
            .Concat(Enumerable.Repeat(Coin.Create(100), expectedOneDollarCoins))
            .Concat(Enumerable.Repeat(Coin.Create(200), expectedTwoDollarCoins))
            .ToArray();

        // Act
        var till = new CoinTill(coins);

        // Assert
        till.TenCentCoins.Should().Be(expectedTenCentCoins);
        till.TwentyCentCoins.Should().Be(expectedTwentyCentCoins);
        till.FiftyCentCoins.Should().Be(expectedFiftyCentCoins);
        till.OneDollarCoins.Should().Be(expectedOneDollarCoins);
        till.TwoDollarCoins.Should().Be(expectedTwoDollarCoins);
    }
}