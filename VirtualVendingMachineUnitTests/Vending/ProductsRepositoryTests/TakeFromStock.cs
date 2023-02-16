using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class TakeFromStock
{
    [Fact]
    public void ReturnsItemOfCorrectProductType()
    {
        // Arrange
        var productsRepository =
            VendingProductsRepositoryBuilder.BuildWithDefaultStock();

        // Act
        var actual = productsRepository.TakeFromStock(Product.Coke);

        // Assert
        actual!.Product.Should().Be(Product.Coke);
    }

    [Fact]
    public void RemovesTakenItemFromStock()
    {
        // Arrange
        var stock = new[] { StockItem.Create(Product.Coke) };
        var productsRepository =
            VendingProductsRepositoryBuilder.Build(withStockItems: stock);

        // Act
        var actual = productsRepository.TakeFromStock(Product.Coke);

        // Assert
        productsRepository.TryFindItemBy(actual!.Id, out _).Should().BeFalse();
    }

    [Fact]
    public void ReturnsNullIfNoItemsOfTypeInStock()
    {
        // Arrange
        var stock = new[] { StockItem.Create(Product.Coke) };
        var productsRepository =
            VendingProductsRepositoryBuilder.Build(withStockItems: stock);

        // Act
        var actual = productsRepository.TakeFromStock(Product.ChocolateBar);

        // Assert
        actual.Should().BeNull();
    }
}