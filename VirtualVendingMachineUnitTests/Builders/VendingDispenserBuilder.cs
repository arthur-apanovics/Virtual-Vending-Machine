using System.Collections.Generic;
using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending;
using VirtualVendingMachineUnitTests.Vending;

namespace VirtualVendingMachineUnitTests.Builders;

public static class VendingDispenserBuilder
{
    public static VendingDispenser Build(
        IVendingProductsRepository? withProductsRepository = null,
        IPricingService? withPricingService = null,
        ICoinTill? withCoinTill = null,
        IEnumerable<Coin>? withChangeBank = null
    ) =>
        new(
            withProductsRepository ??
            VendingProductsRepositoryBuilder.BuildWithDefaultStock(),

            withPricingService ?? PricingServiceBuilder.Build(),
            withCoinTill ?? CoinTillBuilder.Build(),
            withChangeBank ?? TestConstants.VendingDispenser.DefaultChangeBank
        );
}