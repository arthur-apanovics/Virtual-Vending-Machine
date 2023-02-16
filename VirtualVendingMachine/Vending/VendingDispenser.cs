using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using VirtualVendingMachine.Exceptions;
using VirtualVendingMachine.Extensions;
using VirtualVendingMachine.Helpers;
using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Vending;

public class VendingDispenser
{
    public ImmutableArray<Coin> InsertedCoins =>
        _insertedCoins.ToImmutableArray();

    public int InsertedAmountInCents => _insertedCoins.Sum();

    private static readonly int[] SupportedCoins = { 10, 20, 50, 100, 200 };

    private readonly IVendingProductsRepository _productsRepository;
    private readonly CoinTill _coinTill;
    private readonly List<Coin> _insertedCoins = new();


    public VendingDispenser(
        IVendingProductsRepository productsRepository,
        IEnumerable<Coin> changeBank
    )
    {
        _productsRepository = productsRepository;
        _coinTill = new CoinTill(changeBank);
    }

    public ImmutableDictionary<Product, int> ListAvailableProductsAndStock() =>
        _productsRepository.ListStock();

    public void InsertCoin(Coin coin)
    {
        ThrowIfUnsupportedCoin(coin);

        _insertedCoins.Add(coin);
    }

    public DispenseResult Dispense(Product product)
    {
        var productCost = _productsRepository.GetPriceFor(product);
        ThrowIfInsufficientFunds(product, productCost);

        var amountPaid = InsertedAmountInCents;
        TransferInsertedCoinsToTill();
        var change = RetrieveChange(amountPaid, productCost);

        return new DispenseResult(
            product.ToString(),
            productCost,
            change.Sum()
        );
    }

    public IEnumerable<Coin> CancelAndRefund()
    {
        var refund = _insertedCoins.ToArray();
        _insertedCoins.Clear();

        return refund;
    }

    private void TransferInsertedCoinsToTill()
    {
        _coinTill.Add(_insertedCoins);
        _insertedCoins.Clear();
    }

    private Coin[] RetrieveChange(int amountPaid, int productCost)
    {
        return _coinTill.Take(amountPaid - productCost);
    }

    private static void ThrowIfUnsupportedCoin(Coin coin)
    {
        if (!SupportedCoins.Contains(coin.ValueInCents))
            throw new NotSupportedException($"{coin} coins are not supported");
    }

    private void ThrowIfInsufficientFunds(Product product, int requiredAmount)
    {
        if (InsertedAmountInCents < requiredAmount)
            throw new InsufficientFundsException(
                $"Insufficient funds for product \"{product}\" - " +
                $"{CurrencyFormatter.CentsAsCurrency(requiredAmount - InsertedAmountInCents)} required " +
                $"to satisfy product price of {CurrencyFormatter.CentsAsCurrency(requiredAmount)}"
            );
    }
}