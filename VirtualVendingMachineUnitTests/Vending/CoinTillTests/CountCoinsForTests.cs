using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Tills;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.CoinTillTests;

public class CountCoinsForTests
{
    [Theory]
    [MemberData(nameof(CoinBankTestDataProvider))]
    public void ReturnsCorrectAmountForStoredCoins(
        int coinValue,
        Coin[] coins,
        int expectedCoinQty
    )
    {
        // Arrange
        var till = CoinTillBuilder.Build(withCoins: coins);

        // Act
        var actual = till.CountCoinsFor(coinValue);

        // Assert
        actual.Should().Be(expectedCoinQty);
    }

    public static IEnumerable<object[]> CoinBankTestDataProvider()
    {
        yield return new object[] { 10, Array.Empty<Coin>(), 0, };

        var rnd = new Random();

        foreach (var coinValue in TestConstants.CoinTill.AcceptedCoinValues)
        {
            var coinQty = rnd.Next(minValue: 1, maxValue: 100);

            yield return new object[]
            {
                coinValue,
                Enumerable.Repeat(Coin.Create(coinValue), coinQty)
                    .ToArray(),
                coinQty,
            };
        }
    }
}