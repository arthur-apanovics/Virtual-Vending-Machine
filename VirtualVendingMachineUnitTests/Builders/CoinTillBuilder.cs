using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Tills;

namespace VirtualVendingMachineUnitTests.Builders;

public static class CoinTillBuilder
{
    public static CoinTill Build(IEnumerable<Coin>? withCoins = null) =>
        new(withCoins ?? Array.Empty<Coin>());

    public static CoinTill BuildWithStockedCoinBank() =>
        new(
            coins: Array.Empty<Coin>()
                .Concat(Enumerable.Repeat(Coin.Create(10), 50))
                .Concat(Enumerable.Repeat(Coin.Create(20), 40))
                .Concat(Enumerable.Repeat(Coin.Create(50), 30))
                .Concat(Enumerable.Repeat(Coin.Create(100), 20))
                .Concat(Enumerable.Repeat(Coin.Create(200), 10))
                .ToArray()
        );
}