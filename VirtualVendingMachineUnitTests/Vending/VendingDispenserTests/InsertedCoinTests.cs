using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
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
    [MemberData(nameof(AcceptedCoinValues))]
    public void AcceptsSupportedCoinValues(int coin)
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
        var actual = () => dispenser.InsertCoin(123);

        // Assert
        actual.Should().ThrowExactly<NotSupportedException>();
    }

    [Fact]
    public void AccumulatesInsertedCoins()
    {
        // Arrange
        var dispenser = new VendingDispenser(_productsRepository);

        // Act
        dispenser.InsertCoin(10);
        dispenser.InsertCoin(20);
        dispenser.InsertCoin(50);

        // Assert
        dispenser.InsertedCoins.Should().HaveCount(3);
        dispenser.InsertedAmount.Should().Be(80);
    }

    public static IEnumerable<object[]> AcceptedCoinValues()
    {
        yield return new object[] { 10 };
        yield return new object[] { 20 };
        yield return new object[] { 50 };
        yield return new object[] { 100 };
        yield return new object[] { 200 };
    }
}