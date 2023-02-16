using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachineUnitTests.Vending;

public static class TestDataProviders
{
    public static IEnumerable<object[]> VendingProducts() =>
        Product.AllProducts.Select(p => new[] { p });

    public static IEnumerable<object[]> AcceptedCoins() =>
        TestConstants.CoinTill.SupportedCoins.Select(c => new object[] { c });
}