using System;
using VirtualVendingMachine.Exceptions;
using VirtualVendingMachine.Extensions;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Builders;
using VirtualVendingMachine.Tills.Models;

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
        TestHelpers.InsertRequiredFundsFor(expectedItem.Product, dispenser);
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
        TestHelpers.InsertRequiredFundsFor(Product.Coke, dispenser);
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
        TestHelpers.InsertRequiredFundsFor(Product.Coke, dispenser);
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
        TestHelpers.InsertRequiredFundsFor(Product.Coke, dispenser);
        var actual = () => dispenser.Dispense(Product.Coke);

        // Assert
        actual.Should().ThrowExactly<ProductOutOfStockException>();
    }

    [Fact]
    public void ThrowsWhenAttemptingToDispenseUnknownProduct()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build(
            withProductsRepository: VendingProductsRepositoryBuilder
                .BuildWithDefaultStock()
        );
        var unknownProduct = Product.Create("Pepsi");

        // Act
        TestHelpers.InsertRequiredFundsFor(Product.Coke, dispenser);
        var actual = () => dispenser.Dispense(unknownProduct);

        // Assert
        actual.Should()
            .ThrowExactly<NotKnownProductException>()
            .And.UnknownProduct.Should()
            .Be(unknownProduct);
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

        var product = Product.Coke;
        var productCost = TestConstants.Pricing.DefaultPricing[product];
        var insertedCoin = Coin.Create10();

        // Act
        var actual = () =>
        {
            dispenser.InsertCoin(insertedCoin);
            dispenser.Dispense(product);
        };

        // Assert
        actual.Should()
            .Throw<InsufficientFundsException>()
            .Where(ex => ex.ReceivedFunds == insertedCoin.ValueInCents)
            .Where(ex => ex.ExpectedFunds == productCost)
            .Where(ex => ex.ProductName == product.Name);
    }
}