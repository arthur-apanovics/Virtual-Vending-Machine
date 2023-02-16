using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Tills;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.CoinTillTests;

public class TotalValueTests
{
    [Theory]
    [MemberData(nameof(TotalCoinValueProvider))]
    public void ReturnsCorrectTotalValueOfCoinsInTill(
        IEnumerable<Coin> coins,
        int expectedTotalValue
    )
    {
        // Arrange
        var till = CoinTillBuilder.Build(withCoins: coins);

        // Act
        var actual = till.TotalValue;

        // Assert
        actual.Should().Be(expectedTotalValue);
    }

    public static IEnumerable<object[]> TotalCoinValueProvider()
    {
        yield return new object[] { Array.Empty<Coin>(), 0 };
        yield return new object[]
        {
            new[] { Coin.Create10(), Coin.Create20(), Coin.Create100() },
            130
        };
        yield return new object[]
        {
            Enumerable.Repeat(Coin.Create10(), 10), 100
        };
    }
}