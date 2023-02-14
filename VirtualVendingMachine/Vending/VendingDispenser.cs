using System;
using System.Linq;

namespace VirtualVendingMachine.Vending;

public class VendingDispenser
{
    private static readonly int[] SupportedCoins = { 10, 20, 50, 100, 200, };
    private int _till = 0;

    public void InsertCoin(int coinValue)
    {
        ThrowIfUnsupportedCoin(coinValue);

        _till += coinValue;
    }

    public int GetCurrentTillAmount()
    {
        return _till;
    }

    private static void ThrowIfUnsupportedCoin(int coinValue)
    {
        if (!SupportedCoins.Contains(coinValue))
            throw new NotSupportedException(
                $"{coinValue} coins are not supported"
            );
    }
}