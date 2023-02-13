using FluentAssertions;
using VirtualVendingMachine.Entities;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class ListStockTests
{
    [Fact]
    public void ReturnsExpectedAvailableProducts()
    {
        // Arrange
        var repository = new ProductsRepository();

        // Act
        var actual = repository.ListStock();

        // Assert
        var expectedProductsAndQuantity = new (VendingProduct, int)[]
        {
            ( new CokeProduct(), 10 ),
            ( new JuiceProduct(), 9 ),
            ( new ChocolateBarProduct(), 8 ),
        };

        actual.Should().BeEquivalentTo(expectedProductsAndQuantity);
    }
}