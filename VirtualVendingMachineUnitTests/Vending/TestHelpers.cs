using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachineUnitTests.Vending;

public static class TestHelpers
{
    public static void InsertRequiredFundsFor(
        Product product,
        VendingDispenser dispenser,
        int? productPrice = null
    )
    {
        productPrice ??= TestConstants.Pricing.DefaultPricing[product];

        do dispenser.InsertCoin(Coin.Create10());
        while (dispenser.InsertedAmountInCents < productPrice);
    }
}