using System;
using System.Linq;
using VirtualVendingMachine.Extensions;
using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class CollectEarningsTests
{
    [Fact]
    public void ReturnsCorrectAmountOfEarningsFromSales()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build(
            withPricingService: PricingServiceBuilder.Build(
                TestConstants.Pricing.DefaultPricing
            ),
            withChangeBank: Array.Empty<Coin>()
        );
        var productsToPurchase = new[]
        {
            Product.Coke,
            Product.Coke,
            Product.Coke,
            Product.Juice,
            Product.Juice,
            Product.ChocolateBar,
        };
        var expectedEarnings = productsToPurchase.Aggregate(
            0,
            (total, p) => total += TestConstants.Pricing.DefaultPricing[p]
        );

        // Act
        foreach (var product in productsToPurchase)
            Purchase(product, dispenser);

        var actualEarnings = dispenser.CollectEarnings();

        // Assert
        actualEarnings.Sum().Should().Be(expectedEarnings);
    }

    [Fact]
    public void RemovesFundsFromTillAfterCollectingEarnings()
    {
        // Arrange
        var coinTill = CoinTillBuilder.Build(Array.Empty<Coin>());
        var dispenser = VendingDispenserBuilder.Build(
            withPricingService: PricingServiceBuilder.Build(
                TestConstants.Pricing.DefaultPricing
            ),
            withCoinTill: coinTill,
            withChangeBank: Array.Empty<Coin>()
        );
        var productsToPurchase = new[]
        {
            Product.Coke, Product.Coke, Product.Coke,
        };

        // Act
        foreach (var product in productsToPurchase)
            Purchase(product, dispenser);

        dispenser.CollectEarnings();

        // Assert
        coinTill.TotalValue.Should().Be(0);
    }

    private static void Purchase(Product product, VendingDispenser dispenser)
    {
        TestHelpers.InsertRequiredFundsFor(product, dispenser);
        dispenser.Dispense(product);
    }
}