using FluentAssertions;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class GetPriceForTests
{
    [Theory]
    [InlineData(VendingProduct.Coke, 180)]
    [InlineData(VendingProduct.Juice, 220)]
    [InlineData(VendingProduct.ChocolateBar, 300)]
    public void ReturnsExpectedPriceForProduct(
        VendingProduct product,
        int expectedPrice
    )
    {
        // Arrange
        var repository = new ProductsRepository();

        // Act
        var actual = repository.GetPriceFor(product);

        // Assert
        actual.Should().Be(expectedPrice);
    }

    [Theory]
    [MemberData(
        nameof(TestDataProviders.VendingProducts),
        MemberType = typeof(TestDataProviders)
    )]
    public void HasPriceForAllProducts(VendingProduct product)
    {
        // Arrange
        var repository = new ProductsRepository();

        // Act
        var actual = repository.GetPriceFor(product);

        // Assert
        actual.Should().BePositive();
    }
}