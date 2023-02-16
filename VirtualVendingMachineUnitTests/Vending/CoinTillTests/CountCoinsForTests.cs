using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachineUnitTests.Builders;
using VirtualVendingMachine.Tills.Models;

namespace VirtualVendingMachineUnitTests.Vending.CoinTillTests;

public class CountCoinsForTests
{
    [Theory]
    [MemberData(nameof(CoinBankTestDataProvider))]
    public void ReturnsCorrectAmountForStoredCoins(
        Coin coin,
        Coin[] coins,
        int expectedCoinQty
    )
    {
        // Arrange
        var till = CoinTillBuilder.Build(withCoins: coins);

        // Act
        var actual = till.CountCoinsFor(coin.ValueInCents);

        // Assert
        actual.Should().Be(expectedCoinQty);
    }

    public static IEnumerable<object[]> CoinBankTestDataProvider()
    {
        yield return new object[] { Coin.Create10(), Array.Empty<Coin>(), 0, };

        var rnd = new Random();

        foreach (var coin in TestConstants.CoinTill.SupportedCoins)
        {
            var coinQty = rnd.Next(minValue: 1, maxValue: 100);

            yield return new object[]
            {
                coin, Enumerable.Repeat(coin, coinQty).ToArray(), coinQty,
            };
        }
    }
}