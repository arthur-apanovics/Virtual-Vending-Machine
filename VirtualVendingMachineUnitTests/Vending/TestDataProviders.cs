using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending;

namespace VirtualVendingMachineUnitTests.Vending;

public static class TestDataProviders
{
    public static IEnumerable<object[]> VendingProducts() =>
        Enum.GetValues<VendingProduct>()
            .Select(product => new object[] { product });

    public static IEnumerable<object[]> AcceptedCoins() =>
        TestConstants.CoinTill.AcceptedCoinValues.Select(
            coinValue => new object[] { Coin.Create(coinValue) }
        );
}