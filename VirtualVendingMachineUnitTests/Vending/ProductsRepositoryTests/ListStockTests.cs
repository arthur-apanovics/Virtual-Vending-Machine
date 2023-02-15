using FluentAssertions;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class ListStockTests
{
    [Fact]
    public void ReturnsExpectedAvailableProducts()
    {
        // Arrange
        var repository = new VendingProductsRepository();

        // Act
        var actual = repository.ListStock();

        // Assert
        var expectedProductsAndQuantity = new[]
        {
            (VendingProduct.Coke, TestConstants.Stock.CokeDefaultStockQty),
            (VendingProduct.Juice,
                TestConstants.Stock.JuiceDefaultStockQty),
            (VendingProduct.ChocolateBar,
                TestConstants.Stock.ChocolateBarDefaultStockQty),
        };

        actual.Should().BeEquivalentTo(expectedProductsAndQuantity);
    }
}