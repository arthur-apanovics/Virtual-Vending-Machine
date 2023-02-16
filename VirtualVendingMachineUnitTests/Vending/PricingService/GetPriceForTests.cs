using System.Collections.Generic;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Builders;

namespace VirtualVendingMachineUnitTests.Vending.PricingService;

public class GetPriceForTests
{
    [Theory]
    [MemberData(nameof(ProductPricingProvider))]
    public void ReturnsExpectedPriceForProduct(
        Product product,
        int expectedPrice
    )
    {
        // Arrange
        var pricingService = PricingServiceBuilder.Build();

        // Act
        var actual = pricingService.GetPriceFor(product);

        // Assert
        actual.Should().Be(expectedPrice);
    }

    [Theory]
    [MemberData(
        nameof(TestDataProviders.VendingProducts),
        MemberType = typeof(TestDataProviders)
    )]
    public void HasPriceForAllProducts(Product product)
    {
        // Arrange
        var pricingService = PricingServiceBuilder.Build();

        // Act
        var actual = pricingService.GetPriceFor(product);

        // Assert
        actual.Should().BePositive();
    }

    public static IEnumerable<object[]> ProductPricingProvider()
    {
        foreach (var (product, price) in TestConstants.Pricing.DefaultPricing)
            yield return new object[] { product, price };
    }
}