using VirtualVendingMachine.Exceptions;
using VirtualVendingMachine.Extensions;
using VirtualVendingMachine.Tills.Models;

namespace VirtualVendingMachine.Tills;

public interface ICoinTill : ITill<Coin>
{
    int CountCoinsFor(int value);
}

public class CoinTill : ICoinTill
{
    public int TotalValue => _till.Sum();

    private readonly Coin[] _supportedCoins;

    private List<Coin> _till = new();

    public CoinTill(IEnumerable<Coin> supportedCoins)
    {
        _supportedCoins = supportedCoins.ToArray();
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

    public Coin[] Take(int amount)
    {
        ThrowIfInvalidAmountToTake(amount);

        if (amount == 0)
            return Array.Empty<Coin>();

        if (IsSpecificCoinInTill(amount))
            return new[] { TakeCoinFromTill(amount) };

        return TryTakeExactAmountFromTill(amount, out var coins)
            ? coins
            : throw new InsufficientFundsInChangeBankException();
    }

    public IEnumerable<Coin> ListSupportedFundDenominators()
    {
        return _supportedCoins.ToArray();
    }

    private bool TryTakeExactAmountFromTill(int amount, out Coin[] coins)
    {
        var result = new List<Coin>();
        foreach (var coin in _till.ToArray())
        {
            if (result.Sum() == amount)
                break;
            if (coin.ValueInCents > amount)
                continue;
            if (result.Sum() + coin.ValueInCents > amount)
                continue;

            result.Add(TakeCoinFromTill(coin));
        }

        if (result.Sum() == amount)
        {
            coins = result.ToArray();
            return true;
        }

        coins = Array.Empty<Coin>();
        return false;
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

    private void ThrowIfUnsupportedCoin(Coin coin)
    {
        if (!_supportedCoins.Contains(coin))
            throw new NotSupportedCoinException(coin);
    }

    private void ThrowIfInvalidAmountToTake(int amount)
    {
        if (amount < 0)
            throw new ArgumentException(
                "Cannot take negative amounts from till"
            );

        if (amount > 0 && amount < _supportedCoins.Min(c => c.ValueInCents))
            throw new ArgumentException(
                $"Cannot take {amount} from till - " +
                $"smallest denominator supported is {_supportedCoins.Min()}"
            );
    }
}