using System;
using System.Linq;
using VirtualVendingMachine.Helpers;

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

    public object Dispense(VendingProduct product)
    {
        // TODO: Remove hard-coded value
        var requiredTill = 180;
        ThrowIfInsufficientFunds(product, requiredTill);

        // TODO: represent product somehow
        return new
        {
            ProductName = product.ToString(), ProductPrice = requiredTill
        };
    }

    private void ThrowIfInsufficientFunds(
        VendingProduct product,
        int requiredTill
    )
    {
        if (_till < requiredTill)
            throw new InsufficientFundsException(
                $"Insufficient funds for product \"{product}\" - " +
                $"{CurrencyFormatter.CentsAsCurrency(requiredTill - _till)} required " +
                $"to satisfy product price of {CurrencyFormatter.CentsAsCurrency(requiredTill)}"
            );
    }
}