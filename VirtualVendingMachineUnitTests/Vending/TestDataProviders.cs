using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachine.Tills.Models;

namespace VirtualVendingMachineUnitTests.Vending;

public static class TestDataProviders
{
    public static IEnumerable<object[]> VendingProducts() =>
        Product.AllProducts.Select(p => new[] { p });

    public static IEnumerable<object[]> AcceptedCoins() =>
        TestConstants.CoinTill.AcceptedCoinValues.Select(
            coinValue => new object[] { Coin.Create(coinValue) }
        );
}