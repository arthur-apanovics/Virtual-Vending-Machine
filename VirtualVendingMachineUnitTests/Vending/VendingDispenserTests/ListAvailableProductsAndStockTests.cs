using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class ListAvailableProductsAndStockTests
{
    [Fact]
    public void ReturnsExpectedValuesWithDefaultStock()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build(
            withProductsRepository: VendingProductsRepositoryBuilder
                .BuildWithDefaultStock()
        );
        var expectedStock = TestConstants.Stock.DefaultProductQuantities;

        // Act
        var actual = dispenser.ListAvailableProductsAndStock();

        // Assert
        actual.Should().BeEquivalentTo(expectedStock);
    }

    [Fact]
    public void ReturnsEmptyListWhenStockIsEmpty()
    {
        // Arrange
        var dispenser = VendingDispenserBuilder.Build(
            withProductsRepository: VendingProductsRepositoryBuilder.Build()
        );

        // Act
        var actual = dispenser.ListAvailableProductsAndStock();

        // Assert
        actual.Should().BeEmpty();
    }
}