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
        int actual = repository.GetPriceFor(product);

        // Assert
        actual.Should().Be(expectedPrice);
    }
}