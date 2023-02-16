using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class InsertStockTests
{
    [Fact]
    public void CorrectlyUpdatesStock()
    {
        // Arrange
        var productsRepository = VendingProductsRepositoryBuilder.Build();
        var expectedStock = new Dictionary<Product, int>
        {
            {Product.Coke, 14},
            {Product.Juice, 8},
            {Product.ChocolateBar, 23},
        };

        // Act
        foreach (var (product, qty) in expectedStock)
        {
            productsRepository.AddStock(
                Enumerable.Repeat(StockItem.Create(product), qty)
            );
        }
        var actualStock = productsRepository.ListStock();

        // Assert
        actualStock.Should().BeEquivalentTo(expectedStock);
    }
}