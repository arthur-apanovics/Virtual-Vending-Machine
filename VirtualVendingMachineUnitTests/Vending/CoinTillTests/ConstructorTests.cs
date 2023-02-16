using System;
using System.Linq;
using VirtualVendingMachineUnitTests.Builders;
using VirtualVendingMachine.Tills.Models;

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
            .Concat(Enumerable.Repeat(Coin.Create10(), expectedTenCentCoins))
            .Concat(Enumerable.Repeat(Coin.Create20(), expectedTwentyCentCoins))
            .Concat(Enumerable.Repeat(Coin.Create50(), expectedFiftyCentCoins))
            .Concat(Enumerable.Repeat(Coin.Create100(), expectedOneDollarCoins))
            .Concat(Enumerable.Repeat(Coin.Create200(), expectedTwoDollarCoins))
            .ToArray();

        // Act
        var till = CoinTillBuilder.Build(withCoins: coins);

        // Assert
        till.CountCoinsFor(10).Should().Be(expectedTenCentCoins);
        till.CountCoinsFor(20).Should().Be(expectedTwentyCentCoins);
        till.CountCoinsFor(50).Should().Be(expectedFiftyCentCoins);
        till.CountCoinsFor(100).Should().Be(expectedOneDollarCoins);
        till.CountCoinsFor(200).Should().Be(expectedTwoDollarCoins);
    }
}