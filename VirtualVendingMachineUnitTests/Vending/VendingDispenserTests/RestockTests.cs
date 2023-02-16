using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class RestockTests
{
    [Fact]
    public void AddsProvidedItemsToStock()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build(
            withProductsRepository: VendingProductsRepositoryBuilder.Build(
                withStockItems: Array.Empty<StockItem>()
            )
        );
        var expectedProductStock = new Dictionary<Product, int>
        {
            { Product.Coke, 40 },
            { Product.Juice, 20 },
            { Product.ChocolateBar, 15 },
        };
        var items = expectedProductStock.SelectMany(
            kvp => Enumerable.Repeat(StockItem.Create(kvp.Key), kvp.Value)
        );

        // Act
        dispenser.Restock(items);

        // Assert
        dispenser.ListAvailableProductsAndStock()
            .Should()
            .BeEquivalentTo(expectedProductStock);
    }
}