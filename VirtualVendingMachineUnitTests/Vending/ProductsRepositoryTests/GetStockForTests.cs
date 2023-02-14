using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public class GetStockForTests
{
    [Theory]
    [MemberData(nameof(VendingProducts))]
    public void ReturnsPositiveValue(VendingProduct product)
    {
        // Arrange
        var repository = new ProductsRepository();

        // Act
        var actual = repository.GetStockFor(product);

        // Assert
        actual.Should().BePositive();
    }

    public static IEnumerable<object[]> VendingProducts() =>
        Enum.GetValues<VendingProduct>()
            .Select(product => new object[] { product });
}