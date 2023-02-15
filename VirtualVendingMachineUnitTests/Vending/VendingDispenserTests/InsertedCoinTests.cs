using System;
using FluentAssertions;
using VirtualVendingMachine.Tills;
using VirtualVendingMachineUnitTests.Builders;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class InsertedCoinTests
{
    [Theory]
    [MemberData(
        nameof(TestDataProviders.AcceptedCoins),
        MemberType = typeof(TestDataProviders)
    )]
    public void AcceptsSupportedCoinValues(Coin coin)
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build();
        var expectedCoins = new[] { coin };

        // Act
        dispenser.InsertCoin(coin);

        // Assert
        dispenser.InsertedCoins.Should().BeEquivalentTo(expectedCoins);
    }

    [Fact]
    public void ThrowsForUnsupportedCoinValues()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build();

        // Act
        var actual = () => dispenser.InsertCoin(Coin.Create(123));

        // Assert
        actual.Should().ThrowExactly<NotSupportedException>();
    }

    [Fact]
    public void AccumulatesInsertedCoins()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build();

        // Act
        dispenser.InsertCoin(Coin.Create(10));
        dispenser.InsertCoin(Coin.Create(20));
        dispenser.InsertCoin(Coin.Create(50));

        // Assert
        dispenser.InsertedCoins.Should().HaveCount(3);
        dispenser.InsertedAmountInCents.Should().Be(80);
    }
}