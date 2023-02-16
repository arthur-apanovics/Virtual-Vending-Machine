using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class ListStockTests
{
    [Fact]
    public void ReturnsExpectedValuesWithDefaultStock()
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

    [Fact]
    public void ReturnsEmptyListWhenStockIsEmpty()
    {
        // Arrange
        var repository = VendingProductsRepositoryBuilder.Build();

        // Act
        var actual = repository.ListStock();

        // Assert
        actual.Should().BeEmpty();
    }
}