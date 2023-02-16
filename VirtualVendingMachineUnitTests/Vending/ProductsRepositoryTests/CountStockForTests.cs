using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class CountStockForTests
{
    [Theory]
    [MemberData(
        nameof(TestDataProviders.VendingProducts),
        MemberType = typeof(TestDataProviders)
    )]
    public void ReturnsPositiveValue(Product product)
    {
        // Arrange
        var repository =
            VendingProductsRepositoryBuilder.BuildWithDefaultStock();

        // Act
        var actual = repository.CountStockFor(product);

        // Assert
        actual.Should().BePositive();
    }
}