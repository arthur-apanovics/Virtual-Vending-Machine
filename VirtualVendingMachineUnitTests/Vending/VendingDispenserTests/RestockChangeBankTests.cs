using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Tills;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class RestockChangeBankTests
{
    [Fact]
    public void AddsProvidedFundsToChangeBank()
    {
        // Arrange
        var coinTill = CoinTillBuilder.Build(Array.Empty<Coin>());
        var dispenser = VendingDispenserBuilder.Build(
            withCoinTill: coinTill,
            withChangeBank: Array.Empty<Coin>()
        );

        const int expected10CentCoins = 30;
        const int expected20CentCoins = 15;
        var expectedCoins = Array.Empty<Coin>()
            .Concat(Enumerable.Repeat(Coin.Create10(), expected10CentCoins))
            .Concat(Enumerable.Repeat(Coin.Create20(), expected20CentCoins))
            .ToArray();

        // Act
        dispenser.RestockChangeBank(expectedCoins);

        // Assert
        coinTill.CountCoinsFor(10).Should().Be(expected10CentCoins);
        coinTill.CountCoinsFor(20).Should().Be(expected20CentCoins);
    }
}