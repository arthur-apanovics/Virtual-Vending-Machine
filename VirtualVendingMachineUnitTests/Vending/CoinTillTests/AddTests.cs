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
    public void StoresRangeOfAcceptedCoins()
    {
        // Arrange
        var till = new CoinTill(Array.Empty<Coin>());
        var coinRange = new[]
        {
            Coin.Create(10),
            Coin.Create(10),
            Coin.Create(10),
            Coin.Create(50),
            Coin.Create(50),
            Coin.Create(200),
        };

        // Act
        till.Add(coinRange);

        // Assert
        till.CountCoinsFor(10).Should().Be(3);
        till.CountCoinsFor(50).Should().Be(2);
        till.CountCoinsFor(200).Should().Be(1);

        till.CountCoinsFor(20).Should().Be(0);
        till.CountCoinsFor(100).Should().Be(0);
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