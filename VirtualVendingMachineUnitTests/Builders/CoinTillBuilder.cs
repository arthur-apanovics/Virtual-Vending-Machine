using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Tills;

namespace VirtualVendingMachineUnitTests.Builders;

public static class CoinTillBuilder
{
    public static CoinTill Build(IEnumerable<Coin>? withCoins = null)
    {
        var till = new CoinTill();
        if (withCoins is not null)
            till.Add(withCoins);

        return till;
    }

    public static CoinTill BuildWithStockedCoinBank() =>
        Build(
            withCoins: Array.Empty<Coin>()
                .Concat(Enumerable.Repeat(Coin.Create10(), 50))
                .Concat(Enumerable.Repeat(Coin.Create20(), 40))
                .Concat(Enumerable.Repeat(Coin.Create50(), 30))
                .Concat(Enumerable.Repeat(Coin.Create100(), 20))
                .Concat(Enumerable.Repeat(Coin.Create200(), 10))
                .ToArray()
        );
}