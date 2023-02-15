using FluentAssertions;
using VirtualVendingMachine.Vending;
using Xunit;

namespace VirtualVendingMachineUnitTests.Vending.VendingDispenserTests;

public class DispenseTests
{
    [Fact]
    public void DispensesProductWhenPaidForInFull()
    {
        // Arrange
        var dispenser = new VendingDispenser();
        var expectedResult = new
        {
            ProductName = VendingProduct.Coke.ToString(),
            ProductPrice = TestConstants.Pricing.Coke
        };

        // Act
        do dispenser.InsertCoin(10);
        while (dispenser.GetCurrentTillAmount() < TestConstants.Pricing.Coke);

        // Assert
        dispenser.Dispense(VendingProduct.Coke)
            .Should()
            .BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void ThrowsWhenAttemptingToDispenseWithInsufficientTillAmount()
    {
        // Arrange
        var dispenser = new VendingDispenser();

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
            dispenser.Dispense(VendingProduct.Coke);
        };

        // Assert
        actual.Should()
            .Throw<InsufficientFundsException>()
            .WithMessage(expectedMessage);
    }
}