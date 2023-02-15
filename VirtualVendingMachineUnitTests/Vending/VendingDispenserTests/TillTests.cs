using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class TillTests
{
    private readonly IVendingProductsRepository _productsRepository;

    public TillTests()
    {
        _productsRepository =
            Substitute.For<IVendingProductsRepository>();
    }

    [Theory]
    [MemberData(nameof(SupportedCoinValues))]
    public void AcceptsSupportedCoinValues(int coin)
    {
        // Arrange
        var dispenser = new VendingDispenser(_productsRepository);

        // Act
        dispenser.InsertCoin(coin);

        // Assert
        dispenser.GetCurrentTillAmount().Should().Be(coin);
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
    public void AccumulatesTill()
    {
        // Arrange
        var dispenser = new VendingDispenser(_productsRepository);

        // Act
        dispenser.InsertCoin(10);
        dispenser.InsertCoin(20);
        dispenser.InsertCoin(50);

        // Assert
        dispenser.GetCurrentTillAmount().Should().Be(80);
    }

    public static IEnumerable<object[]> SupportedCoinValues()
    {
        yield return new object[] { 10 };
        yield return new object[] { 20 };
        yield return new object[] { 50 };
        yield return new object[] { 100 };
        yield return new object[] { 200 };
    }
}