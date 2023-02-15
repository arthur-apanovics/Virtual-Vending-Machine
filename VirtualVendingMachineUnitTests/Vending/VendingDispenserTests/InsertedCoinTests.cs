using System;
using FluentAssertions;
using NSubstitute;
using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class InsertedCoinTests
{
    private readonly IVendingProductsRepository _productsRepository;

    public InsertedCoinTests()
    {
        _productsRepository = Substitute.For<IVendingProductsRepository>();
    }

    [Theory]
    [MemberData(
        nameof(TestDataProviders.AcceptedCoins),
        MemberType = typeof(TestDataProviders)
    )]
    public void AcceptsSupportedCoinValues(Coin coin)
    {
        // Arrange
        var dispenser = new VendingDispenser(_productsRepository);
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
        var dispenser = new VendingDispenser(_productsRepository);

        // Act
        var actual = () => dispenser.InsertCoin(Coin.Create(123));

        // Assert
        actual.Should().ThrowExactly<NotSupportedException>();
    }

    [Fact]
    public void AccumulatesInsertedCoins()
    {
        // Arrange
        var dispenser = new VendingDispenser(_productsRepository);

        // Act
        dispenser.InsertCoin(Coin.Create(10));
        dispenser.InsertCoin(Coin.Create(20));
        dispenser.InsertCoin(Coin.Create(50));

        // Assert
        dispenser.InsertedCoins.Should().HaveCount(3);
        dispenser.InsertedAmountInCents.Should().Be(80);
    }
}