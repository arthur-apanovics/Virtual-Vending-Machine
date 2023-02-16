using VirtualVendingMachine.Tills.Models;

namespace VirtualVendingMachine.Extensions;

public static class CoinExtensions
{
    public static int Sum(this IEnumerable<Coin> coins)
    {
        // ReSharper disable once InvokeAsExtensionMethod
        return Enumerable.Sum(coins, c => c.ValueInCents);
    }
}