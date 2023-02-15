using System;
using System.Linq;
using VirtualVendingMachine.Helpers;

namespace VirtualVendingMachine.Vending;

public class VendingDispenser
{
    private static readonly int[] SupportedCoins = { 10, 20, 50, 100, 200, };

    private readonly IVendingProductsRepository _productsRepository;
    private int _till;

    public VendingDispenser(IVendingProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;

        ResetTill();
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

    public DispenseResult Dispense(VendingProduct product)
    {
        var productPrice = _productsRepository.GetPriceFor(product);
        ThrowIfInsufficientFunds(product, productPrice);

        var change = CalculateChange(productPrice);
        ResetTill();

        return new DispenseResult(product.ToString(), productPrice, change);
    }

    private int CalculateChange(int productPrice)
    {
        return _till - productPrice;
    }

    private void ResetTill()
    {
        _till = 0;
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