using System.Collections.Generic;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class ListStockTests
{
    [Fact]
    public void ReturnsExpectedAvailableProducts()
    {
        // Arrange
        var repository =
            VendingProductsRepositoryBuilder.BuildWithDefaultStock();
        var expectedProductsAndQuantity = new Dictionary<Product, int>
        {
            { Product.Coke, TestConstants.Stock.CokeDefaultStockQty },
            { Product.Juice, TestConstants.Stock.JuiceDefaultStockQty },
            {
                Product.ChocolateBar,
                TestConstants.Stock.ChocolateBarDefaultStockQty
            },
        };

        // Act
        var actual = repository.ListStock();

        // Assert
        actual.Should().BeEquivalentTo(expectedProductsAndQuantity);
    }
}