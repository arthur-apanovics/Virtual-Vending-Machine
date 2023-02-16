using VirtualVendingMachine.Vending;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class GetStockForTests
{
    [Theory]
    [MemberData(
        nameof(TestDataProviders.VendingProducts),
        MemberType = typeof(TestDataProviders)
    )]
    public void ReturnsPositiveValue(Product product)
    {
        // Arrange
        var repository = new VendingProductsRepository();

        // Act
        var actual = repository.GetStockFor(product);

        // Assert
        actual.Should().BePositive();
    }
}