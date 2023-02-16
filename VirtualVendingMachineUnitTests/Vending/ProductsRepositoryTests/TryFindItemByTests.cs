using System;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class TryFindItemByTests
{
    [Fact]
    public void ReturnsTrueAndCorrectProductWhenItemExists()
    {
        // Arrange
        var item = StockItem.Create(Product.Coke);
        var productsRepository = VendingProductsRepositoryBuilder.Build(
            withStockItems: new[] { item }
        );

        // Act
        var actualResult = productsRepository.TryFindItemBy(
            item.Id,
            out var actualProduct
        );

        // Assert
        actualResult.Should().BeTrue();
        actualProduct.Should().Be(Product.Coke);
    }

    [Fact]
    public void ReturnsFalseAndNullWhenItemDoesNotExist()
    {
        // Arrange
        var productsRepository =
            VendingProductsRepositoryBuilder.BuildWithDefaultStock();

        // Act
        var isFound = productsRepository.TryFindItemBy(
            Guid.NewGuid(),
            out var product
        );

        // Assert
        isFound.Should().BeFalse();
        product.Should().BeNull();
    }

    [Fact]
    public void DoesNotRemoveItemFromStockAfterRetrieval()
    {
        // Arrange
        var item = StockItem.Create(Product.Coke);
        var productsRepository = VendingProductsRepositoryBuilder.Build(
            withStockItems: new[] { item }
        );

        // Act
        productsRepository.TryFindItemBy(item.Id, out var product);

        // Assert
        productsRepository.CountStockFor(product!).Should().Be(1);
    }
}