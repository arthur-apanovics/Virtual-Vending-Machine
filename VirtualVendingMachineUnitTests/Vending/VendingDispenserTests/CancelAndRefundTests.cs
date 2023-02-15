using System.Linq;
using VirtualVendingMachine.Extensions;
using VirtualVendingMachine.Tills;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class CancelAndRefundTests
{
    [Fact]
    public void RefundsInsertedAmount()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build();
        var expectedCoins = new[]
        {
            Coin.Create(10),
            Coin.Create(20),
            Coin.Create(50),
            Coin.Create(100),
        };

        // Act
        foreach (var coin in expectedCoins)
            dispenser.InsertCoin(coin);

        var actual = dispenser.CancelAndRefund().ToArray();

        // Assert
        actual.Should().BeEquivalentTo(expectedCoins);
        actual.Sum().Should().Be(180);
    }

    [Fact]
    public void RemovesCoinsFromCoinHolderAfterRefund()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build();

        // Act
        dispenser.InsertCoin(Coin.Create(50));
        dispenser.InsertCoin(Coin.Create(20));
        dispenser.InsertCoin(Coin.Create(100));
        dispenser.CancelAndRefund();

        // Assert
        dispenser.InsertedCoins.Should().BeEmpty();
        dispenser.InsertedAmountInCents.Should().Be(0);
    }
}