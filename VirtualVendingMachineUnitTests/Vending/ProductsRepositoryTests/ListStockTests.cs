using FluentAssertions;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class ListStockTests
{
    private const int CokeDefaultStockQty = 10;
    private const int JuiceDefaultStockQty = 9;
    private const int ChocolateBarDefaultStockQty = 8;

    [Fact]
    public void ReturnsExpectedAvailableProducts()
    {
        // Arrange
        var repository = new ProductsRepository();

        // Act
        var actual = repository.ListStock();

        // Assert
        var expectedProductsAndQuantity = new[]
        {
            (VendingProduct.Coke, CokeDefaultStockQty),
            (VendingProduct.Juice, JuiceDefaultStockQty),
            (VendingProduct.ChocolateBar, ChocolateBarDefaultStockQty),
        };

        actual.Should().BeEquivalentTo(expectedProductsAndQuantity);
    }
}