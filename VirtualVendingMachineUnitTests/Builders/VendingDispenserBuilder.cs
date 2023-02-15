using System.Collections.Generic;
using NSubstitute;
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
            Substitute.For<IVendingProductsRepository>(),
            withCoinBank ?? TestConstants.VendingDispenser.DefaultChangeBank
        );
}