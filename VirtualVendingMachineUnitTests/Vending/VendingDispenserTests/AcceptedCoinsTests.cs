using System.Linq;
using VirtualVendingMachine.Tills.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class AcceptedCoinsTests
{
    [Fact]
    public void IntersectsTillAndCoinSlotAcceptedValues()
    {
        // Arrange
        var tillSupportedCoins =
            new[] { 5, 10, 15, 20, 200 }.Select(Coin.Create).ToArray();
        // Assuming default values of {10, 20, 50, 100, 200} for dispenser
        var expectedCoinValues = new[] { 10, 20, 200 };

        var dispenser = VendingDispenserBuilder.Build(
            withCoinTill: CoinTillBuilder.Build(
                withSupportedCoins: tillSupportedCoins
            )
        );

        // Act
        var actualCoins = dispenser.AcceptedCoins;

        // Assert
        actualCoins.Should()
            .BeEquivalentTo(expectedCoinValues.Select(Coin.Create));
    }
}