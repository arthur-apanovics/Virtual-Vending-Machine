using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtualVendingMachine.Tills;

public class CoinTill
{
    public int TenCentCoins => _coins.Count(c => c.ValueInCents == 10);
    public int TwentyCentCoins => _coins.Count(c => c.ValueInCents == 20);
    public int FiftyCentCoins => _coins.Count(c => c.ValueInCents == 50);
    public int OneDollarCoins => _coins.Count(c => c.ValueInCents == 100);
    public int TwoDollarCoins => _coins.Count(c => c.ValueInCents == 200);

    private static readonly int[] SupportedCoins = { 10, 20, 50, 100, 200 };

    private readonly List<Coin> _coins;

    public CoinTill(IEnumerable<Coin> coins)
    {
        _coins = coins.ToList();
    }

    public int CountCoinsFor(int value) =>
        _coins.Count(c => c.ValueInCents == value);

    public void Add(Coin coin)
    {
        ThrowIfUnsupportedCoin(coin);
        _coins.Add(coin);
    }

    private static void ThrowIfUnsupportedCoin(Coin coin)
    {
        if (!SupportedCoins.Contains(coin.ValueInCents))
            throw new NotSupportedException(
                $"{nameof(CoinTill)} does not support {coin} coins"
            );
    }
}