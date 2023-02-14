using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Vending;

namespace VirtualVendingMachineUnitTests.Vending.ProductsRepositoryTests;

public static class TestDataProviders
{
    public static IEnumerable<object[]> VendingProducts() =>
        Enum.GetValues<VendingProduct>()
            .Select(product => new object[] { product });
}