using System.Linq;
using VirtualVendingMachine.Tills.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.CoinTillTests;

public class ListSupportedFundDenominators
{
    [Fact]
    public void ReturnsExpectedValues()
    {
        // Arrange
        var expectedCoins =
            new[] { 1, 2, 3, 4, 5 }.Select(Coin.Create).ToArray();
        var till = CoinTillBuilder.Build(withSupportedCoins: expectedCoins);

        // Act
        var actualValues = till.ListSupportedFundDenominators();

        // Assert
        actualValues.Should().BeEquivalentTo(expectedCoins);
    }
}