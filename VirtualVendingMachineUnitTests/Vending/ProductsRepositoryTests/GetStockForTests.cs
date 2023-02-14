using FluentAssertions;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class GetStockForTests
{
    [Theory]
    [MemberData(
        nameof(TestDataProviders.VendingProducts),
        MemberType = typeof(TestDataProviders)
    )]
    public void ReturnsPositiveValue(VendingProduct product)
    {
        // Arrange
        var repository = new VendingProductsRepository();

        // Act
        var actual = repository.GetStockFor(product);

        // Assert
        actual.Should().BePositive();
    }
}