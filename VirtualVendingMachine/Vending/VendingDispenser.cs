using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using VirtualVendingMachine.Helpers;

namespace VirtualVendingMachine.Vending;

public class VendingDispenser
{
    public ImmutableArray<int> InsertedCoins =>
        _insertedCoins.ToImmutableArray();
    public int InsertedAmount => _insertedCoins.Sum();

    private static readonly int[] SupportedCoins = { 10, 20, 50, 100, 200, };

    private readonly IVendingProductsRepository _productsRepository;
    private readonly List<int> _insertedCoins = new();


    public VendingDispenser(IVendingProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public void InsertCoin(int coinValue)
    {
        ThrowIfUnsupportedCoin(coinValue);

        _insertedCoins.Add(coinValue);
    }

    public DispenseResult Dispense(VendingProduct product)
    {
        var productPrice = _productsRepository.GetPriceFor(product);
        ThrowIfInsufficientFunds(product, productPrice);

        var change = RetrieveChange(productPrice);

        return new DispenseResult(product.ToString(), productPrice, change);
    }

    // TODO: return individual coins
    private int RetrieveChange(int productPrice)
    {
        return InsertedAmount - productPrice;
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
        int requiredAmount
    )
    {
        if (InsertedAmount < requiredAmount)
            throw new InsufficientFundsException(
                $"Insufficient funds for product \"{product}\" - " +
                $"{CurrencyFormatter.CentsAsCurrency(requiredAmount - InsertedAmount)} required " +
                $"to satisfy product price of {CurrencyFormatter.CentsAsCurrency(requiredAmount)}"
            );
    }
}