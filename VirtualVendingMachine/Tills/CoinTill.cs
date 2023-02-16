using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Exceptions;
using VirtualVendingMachine.Extensions;

namespace VirtualVendingMachine.Tills;

public class CoinTill
{
    public int TotalValue => _till.Sum();

    private static readonly int[] SupportedCoins = { 10, 20, 50, 100, 200 };

    private List<Coin> _till;


    public CoinTill(IEnumerable<Coin> coins)
    {
        _till = coins.ToList();
    }

    public int CountCoinsFor(int value) =>
        _till.Count(c => c.ValueInCents == value);

    public void Add(Coin coin)
    {
        ValidateAndAddCoinToTill(coin);
        SortCoinsInTill();
    }

    public void Add(IEnumerable<Coin> coins)
    {
        foreach (var coin in coins)
            ValidateAndAddCoinToTill(coin);

        SortCoinsInTill();
    }

    private void ValidateAndAddCoinToTill(Coin coin)
    {
        ThrowIfUnsupportedCoin(coin);
        _till.Add(coin);
    }

    private void SortCoinsInTill()
    {
        _till = _till.OrderByDescending(c => c.ValueInCents).ToList();
    }

    private static void ThrowIfUnsupportedCoin(Coin coin)
    {
        if (!SupportedCoins.Contains(coin.ValueInCents))
            throw new NotSupportedException(
                $"{nameof(CoinTill)} does not support {coin} coins"
            );
    }

    public Coin[] Take(int amount)
    {
        if (amount < 0)
            throw new ArgumentException(
                "Cannot take negative amounts from till"
            );

        if (amount == 0)
            return Array.Empty<Coin>();

        if (amount < SupportedCoins.Min())
            throw new ArgumentException(
                $"Cannot take {amount} from till - " +
                $"smallest denominator supported is {SupportedCoins.Min()}"
            );

        if (IsSpecificCoinInTill(amount))
            return new[] { TakeCoinFromTill(amount) };

        var result = new List<Coin>();
        var tillCopy = new Coin[_till.Count];
        _till.CopyTo(tillCopy);
        foreach (var coin in tillCopy)
        {
            if (result.Sum() == amount)
                return result.ToArray();

            if (coin.ValueInCents < amount)
                result.Add(TakeCoinFromTill(coin));
        }

        throw new InsufficientFundsInChangeBankException();
    }

    private bool IsSpecificCoinInTill(int value)
    {
        return _till.Any(c => c.ValueInCents == value);
    }

    private Coin TakeCoinFromTill(int value)
    {
        return TakeCoinFromTill(Coin.Create(value));
    }

    private Coin TakeCoinFromTill(Coin coin)
    {
        var isRemoved = _till.Remove(coin);
        if (!isRemoved)
            throw new InvalidOperationException(
                $"Failed to remove {coin} coin from till"
            );

        return coin;
    }
}