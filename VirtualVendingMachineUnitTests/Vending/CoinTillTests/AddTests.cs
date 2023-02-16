using System;
using VirtualVendingMachine.Exceptions;
using VirtualVendingMachineUnitTests.Builders;
using VirtualVendingMachine.Tills.Models;

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
        var till = CoinTillBuilder.Build();

        // Act
        till.Add(coin);

        // Assert
        till.CountCoinsFor(coin.ValueInCents).Should().Be(1);
    }

    [Fact]
    public void StoresRangeOfAcceptedCoins()
    {
        // Arrange
        var till = CoinTillBuilder.Build();
        var coinRange = new[]
        {
            Coin.Create10(),
            Coin.Create10(),
            Coin.Create10(),
            Coin.Create50(),
            Coin.Create50(),
            Coin.Create200(),
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
        var till = CoinTillBuilder.Build();

        // Act
        var actual = () => till.Add(Coin.Create(321));

        // Assert
        actual.Should().ThrowExactly<NotSupportedCoinException>();
    }
}