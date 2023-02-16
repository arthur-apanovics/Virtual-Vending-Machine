using System;
using VirtualVendingMachine.Exceptions;
using VirtualVendingMachine.Extensions;
using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class DispenseTests
{
    [Fact]
    public void DispensesItemWhenPaidForInFull()
    {
        // Arrange
        var expectedItem = StockItem.Create(Product.Coke);
        var dispenser = VendingDispenserBuilder.Build(
            VendingProductsRepositoryBuilder.Build(
                withStockItems: new[] { expectedItem }
            ),
            PricingServiceBuilder.Build(
                withPricing: TestConstants.Pricing.DefaultPricing
            )
        );

        // Act
        InsertRequiredFundsFor(expectedItem.Product, dispenser);
        var actual = dispenser.Dispense(expectedItem.Product);

        // Assert
        actual.Item.Should().Be(expectedItem);
    }

    [Fact]
    public void ResetsInsertedCoinsAfterDispensing()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build();

        // Act
        InsertRequiredFundsFor(Product.Coke, dispenser);
        dispenser.Dispense(Product.Coke);

        // Assert
        dispenser.InsertedCoins.Should().BeEmpty();
        dispenser.InsertedAmountInCents.Should().Be(0);
    }

    [Fact]
    public void ReturnsCorrectChange()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build(
            withPricingService: PricingServiceBuilder.Build(
                withPricing: TestConstants.Pricing.DefaultPricing
            )
        );
        const int overfillAmount = 50;

        // Act
        InsertRequiredFundsFor(Product.Coke, dispenser);
        dispenser.InsertCoin(Coin.Create(overfillAmount));
        var result = dispenser.Dispense(Product.Coke);

        // Assert
        result.Change.Sum().Should().Be(overfillAmount);
    }

    [Fact]
    public void ThrowsWhenProductNotInStock()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build(
            withProductsRepository: VendingProductsRepositoryBuilder.Build(
                withStockItems: Array.Empty<StockItem>()
            )
        );

        // Act
        InsertRequiredFundsFor(Product.Coke, dispenser);
        var actual = () => dispenser.Dispense(Product.Coke);

        // Assert
        actual.Should().ThrowExactly<ProductOutOfStockException>();
    }

    [Fact]
    public void ThrowsWhenAttemptingToDispenseWithInsufficientFunds()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build(
            withPricingService: PricingServiceBuilder.Build(
                withPricing: TestConstants.Pricing.DefaultPricing
            )
        );

        // hard-coded values to avoid invoking business logic
        var insertedCoin = Coin.Create(10);
        const string expectedProductName = "Coke";
        const string expectedFullPrice = "$1.80";
        const string expectedFillAmount = "$1.70";
        const string expectedMessage =
            $"Insufficient funds for product \"{expectedProductName}\" - " +
            $"{expectedFillAmount} required to satisfy product price of {expectedFullPrice}";

        // Act
        var actual = () =>
        {
            dispenser.InsertCoin(insertedCoin);
            dispenser.Dispense(Product.Coke);
        };

        // Assert
        actual.Should()
            .Throw<InsufficientFundsException>()
            .WithMessage(expectedMessage);
    }

    private static void InsertRequiredFundsFor(
        Product product,
        VendingDispenser dispenser
    )
    {
        do dispenser.InsertCoin(Coin.Create(10));
        while (dispenser.InsertedAmountInCents <
               TestConstants.Pricing.DefaultPricing[product]);
    }
}