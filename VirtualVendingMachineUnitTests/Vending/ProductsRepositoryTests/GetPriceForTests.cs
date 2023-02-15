using FluentAssertions;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class GetPriceForTests
{
    [Theory]
    [InlineData(VendingProduct.Coke, TestConstants.Pricing.Coke)]
    [InlineData(VendingProduct.Juice, TestConstants.Pricing.Juice)]
    [InlineData(
        VendingProduct.ChocolateBar,
        TestConstants.Pricing.ChocolateBar
    )]
    public void ReturnsExpectedPriceForProduct(
        VendingProduct product,
        int expectedPrice
    )
    {
        // Arrange
        var repository = new VendingProductsRepository();

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
        var repository = new VendingProductsRepository();

        // Act
        var actual = repository.GetPriceFor(product);

        // Assert
        actual.Should().BePositive();
    }
}