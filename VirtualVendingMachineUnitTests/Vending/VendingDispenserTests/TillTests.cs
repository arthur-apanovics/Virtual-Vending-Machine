using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class TillTests
{
    [Theory]
    [MemberData(nameof(SupportedCoinValues))]
    public void AcceptsSupportedCoinValues(int coin)
    {
        // Arrange
        var dispenser = new VirtualVendingMachine.Vending.VendingDispenser();

        // Act
        dispenser.InsertCoin(coin);

        // Assert
        dispenser.GetCurrentTillAmount().Should().Be(coin);
    }

    [Fact]
    public void ThrowsForUnsupportedCoinValues()
    {
        // Arrange
        var dispenser = new VirtualVendingMachine.Vending.VendingDispenser();

        // Act
        var actual = () => dispenser.InsertCoin(123);

        // Assert
        actual.Should().ThrowExactly<NotSupportedException>();
    }

    [Fact]
    public void AccumulatesTill()
    {
        // Arrange
        var dispenser = new VirtualVendingMachine.Vending.VendingDispenser();

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