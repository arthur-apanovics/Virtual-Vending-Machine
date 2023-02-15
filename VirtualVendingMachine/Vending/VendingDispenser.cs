using System;
using System.Linq;
using VirtualVendingMachine.Helpers;

namespace VirtualVendingMachine.Vending;

public class VendingDispenser
{
    private static readonly int[] SupportedCoins = { 10, 20, 50, 100, 200, };

    private readonly IVendingProductsRepository _productsRepository;
    private int _till = 0;

    public VendingDispenser(IVendingProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public void InsertCoin(int coinValue)
    {
        ThrowIfUnsupportedCoin(coinValue);

        _till += coinValue;
    }

    public int GetCurrentTillAmount()
    {
        return _till;
    }

    public object Dispense(VendingProduct product)
    {
        var productPrice = _productsRepository.GetPriceFor(product);
        ThrowIfInsufficientFunds(product, productPrice);

        // TODO: Return change

        // TODO: represent product somehow
        return new
        {
            ProductName = product.ToString(), ProductPrice = productPrice
        };
    }

    private static void ThrowIfUnsupportedCoin(int coinValue)
    {
        if (!SupportedCoins.Contains(coinValue))
            throw new NotSupportedException(
                $"{coinValue} coins are not supported"
            );
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