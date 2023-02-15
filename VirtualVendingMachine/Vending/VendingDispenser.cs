using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using VirtualVendingMachine.Helpers;
using VirtualVendingMachine.Tills;

namespace VirtualVendingMachine.Vending;

public class VendingDispenser
{
    public ImmutableArray<Coin> InsertedCoins =>
        _insertedCoins.ToImmutableArray();

    public int InsertedAmountInCents => _insertedCoins.Sum(c => c.ValueInCents);

    private static readonly int[] SupportedCoins = { 10, 20, 50, 100, 200 };

    private readonly IVendingProductsRepository _productsRepository;
    private readonly List<Coin> _insertedCoins = new();


    public VendingDispenser(IVendingProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public void InsertCoin(Coin coin)
    {
        ThrowIfUnsupportedCoin(coin);

        _insertedCoins.Add(coin);
    }

    public DispenseResult Dispense(VendingProduct product)
    {
        var productPrice = _productsRepository.GetPriceFor(product);
        ThrowIfInsufficientFunds(product, productPrice);

        TransferCoinsToTill();
        var change = RetrieveChange(productPrice);

        return new DispenseResult(product.ToString(), productPrice, change);
    }

    private void TransferCoinsToTill()
    {
        for (var i = 0; i < _insertedCoins.Count; i++)
        {
            // _coinTill.Store(_insertedCoins[i]);
            _insertedCoins.RemoveAt(i);
        }
    }

    private int RetrieveChange(int productPrice)
    {
        // TODO: return individual coins
        throw new NotImplementedException();
    }

    private static void ThrowIfUnsupportedCoin(Coin coin)
    {
        if (!SupportedCoins.Contains(coin.ValueInCents))
            throw new NotSupportedException($"{coin} coins are not supported");
    }

    private void ThrowIfInsufficientFunds(
        VendingProduct product,
        int requiredAmount
    )
    {
        if (InsertedAmountInCents < requiredAmount)
            throw new InsufficientFundsException(
                $"Insufficient funds for product \"{product}\" - " +
                $"{CurrencyFormatter.CentsAsCurrency(requiredAmount - InsertedAmountInCents)} required " +
                $"to satisfy product price of {CurrencyFormatter.CentsAsCurrency(requiredAmount)}"
            );
    }
}