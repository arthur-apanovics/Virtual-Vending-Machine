using System;
using FluentAssertions;
using VirtualVendingMachine.Tills;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.CoinTillTests;

public class AddTests
{
    [Theory]
    [MemberData(
        nameof(TestDataProviders.AcceptedCoins),
        MemberType = typeof(TestDataProviders)
    )]
    public void StoresAcceptedCoins(Coin coin)
    {
        // Arrange
        var till = new CoinTill(Array.Empty<Coin>());

        // Act
        till.Add(coin);

        // Assert
        till.CountCoinsFor(coin.ValueInCents).Should().Be(1);
    }

    [Fact]
    public void ThrowsWhenStoringUnsupportedCoin()
    {
        // Arrange
        var till = new CoinTill(Array.Empty<Coin>());

        // Act
        var actual = () => till.Add(Coin.Create(321));

        // Assert
        actual.Should().ThrowExactly<NotSupportedException>();
    }
}