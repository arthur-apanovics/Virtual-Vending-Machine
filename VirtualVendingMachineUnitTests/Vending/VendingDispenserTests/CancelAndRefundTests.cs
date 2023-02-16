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
            Coin.Create10(),
            Coin.Create20(),
            Coin.Create50(),
            Coin.Create100(),
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
        dispenser.InsertCoin(Coin.Create50());
        dispenser.InsertCoin(Coin.Create20());
        dispenser.InsertCoin(Coin.Create100());
        dispenser.CancelAndRefund();

        // Assert
        dispenser.InsertedCoins.Should().BeEmpty();
        dispenser.InsertedAmountInCents.Should().Be(0);
    }
}