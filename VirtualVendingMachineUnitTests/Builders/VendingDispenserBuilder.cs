using System.Collections.Generic;
using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending;
using VirtualVendingMachineUnitTests.Vending;

namespace VirtualVendingMachineUnitTests.Builders;

public static class VendingDispenserBuilder
{
    public static VendingDispenser Build(
        IVendingProductsRepository? withProductsRepository = null,
        IEnumerable<Coin>? withCoinBank = null
    ) =>
        new(
            withProductsRepository ??
            VendingProductsRepositoryBuilder.BuildWithDefaultStock(),
            withCoinBank ?? TestConstants.VendingDispenser.DefaultChangeBank
        );
}