using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using VirtualVendingMachine.Tills;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.CoinTillTests;

public class CountCoinsForTests
{
    [Theory]
    [MemberData(nameof(CoinDataProvider))]
    public void ReturnsCorrectAmountForStoredCoins(
        int coinValue,
        Coin[] coins,
        int expectedAmount
    )
    {
        // Arrange
        var till = new CoinTill(coins);

        // Act
        var actual = till.CountCoinsFor(coinValue);

        // Assert
        actual.Should().Be(expectedAmount);
    }

    public static IEnumerable<object[]> CoinDataProvider()
    {
        yield return new object[]
        {
            10,
            Array.Empty<Coin>(),
            0,
        };

        var rnd = new Random();

        foreach (var coinValue in TestConstants.CoinTill.AcceptedCoinValues)
        {
            var amount = rnd.Next();

            yield return new object[]
            {
                coinValue,
                Enumerable.Repeat(Coin.Create(coinValue), amount).ToArray(),
                amount,
            };
        }
    }
}