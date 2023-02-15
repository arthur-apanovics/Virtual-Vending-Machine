using FluentAssertions;
using NSubstitute;
using VirtualVendingMachine.Tills;
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
        var expectedResult = new DispenseResult(
            ProductUnderTest.ToString(),
            PriceForProductUnderTest,
            Change: 0
        );

        // Act
        FillCoinHolderWIthRequiredAmount(dispenser);

        // Assert
        dispenser.Dispense(ProductUnderTest)
            .Should()
            .BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void ResetsInsertedCoinsAfterDispensing()
    {
        // Arrange
        var dispenser = new VendingDispenser(_productsRepository);

        // Act
        FillCoinHolderWIthRequiredAmount(dispenser);
        dispenser.Dispense(ProductUnderTest);

        // Assert
        dispenser.InsertedCoins.Should().BeEmpty();
        dispenser.InsertedAmountInCents.Should().Be(0);
    }


    [Fact]
    public void ReturnsCorrectChange()
    {
        // Arrange
        var dispenser = new VendingDispenser(_productsRepository);
        const int overfillAmount = 50;

        // Act
        FillCoinHolderWIthRequiredAmount(dispenser);
        dispenser.InsertCoin(Coin.Create(overfillAmount));
        var result = dispenser.Dispense(ProductUnderTest);

        // Assert
        result.Change.Should().Be(overfillAmount);
    }

    [Fact]
    public void ThrowsWhenAttemptingToDispenseWithInsufficientFunds()
    {
        // Arrange
        var dispenser = new VendingDispenser(_productsRepository);

        // hard-coded values to avoid invoking business logic
        var insertedCoin = Coin.Create(10);
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

    private static void FillCoinHolderWIthRequiredAmount(VendingDispenser dispenser)
    {
        do dispenser.InsertCoin(Coin.Create(10));
        while (dispenser.InsertedAmountInCents < PriceForProductUnderTest);
    }
}