using FluentAssertions;
using NSubstitute;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class DispenseTests
{
    private readonly IVendingProductsRepository _productsRepository;

    private const VendingProduct ProductUnderTest = VendingProduct.Coke;
    private const int PriceForProductUnderTest = TestConstants.Pricing.Coke;

    public DispenseTests()
    {
        _productsRepository = Substitute.For<IVendingProductsRepository>();
        _productsRepository.GetPriceFor(ProductUnderTest)
            .Returns(PriceForProductUnderTest);
    }

    [Fact]
    public void DispensesProductWhenPaidForInFull()
    {
        // Arrange
        var dispenser = new VendingDispenser(_productsRepository);
        var expectedResult = new
        {
            ProductName = ProductUnderTest.ToString(),
            ProductPrice = PriceForProductUnderTest
        };

        // Act
        do dispenser.InsertCoin(10);
        while (dispenser.GetCurrentTillAmount() < PriceForProductUnderTest);

        // Assert
        dispenser.Dispense(ProductUnderTest)
            .Should()
            .BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void ThrowsWhenAttemptingToDispenseWithInsufficientTillAmount()
    {
        // Arrange
        var dispenser = new VendingDispenser(_productsRepository);

        // hard-coded values to avoid invoking business logic
        const int insertedCoin = 10;
        const string expectedProductName = "Coke";
        const string expectedFullPrice = "$1.80";
        const string expectedFillAmount = "$1.70";
        const string expectedMessage =
            $"Insufficient funds for product \"{expectedProductName}\" - " +
            $"{expectedFillAmount} required to satisfy product price of {expectedFullPrice}";

        // Act
        var actual = () =>
        {
            dispenser.InsertCoin(insertedCoin);
            dispenser.Dispense(ProductUnderTest);
        };

        // Assert
        actual.Should()
            .Throw<InsufficientFundsException>()
            .WithMessage(expectedMessage);
    }
}