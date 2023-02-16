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

        // Act
        var actual = repository.ListStock();

        // Assert
        actual.Should()
            .BeEquivalentTo(TestConstants.Stock.DefaultProductQuantities);
    }
}