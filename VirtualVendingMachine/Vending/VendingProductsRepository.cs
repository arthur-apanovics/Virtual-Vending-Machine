using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace VirtualVendingMachine.Vending;

public class VendingProductsRepository : IVendingProductsRepository
{
    private readonly ImmutableDictionary<VendingProduct, int> _stock;
    private readonly ImmutableDictionary<VendingProduct, int> _pricing;

    public VendingProductsRepository()
    {
        _stock = new Dictionary<VendingProduct, int>
        {
            { VendingProduct.Coke, 10 },
            { VendingProduct.Juice, 9 },
            { VendingProduct.ChocolateBar, 8 },
        }.ToImmutableDictionary();

        _pricing = new Dictionary<VendingProduct, int>
        {
            { VendingProduct.Coke, 180 },
            { VendingProduct.Juice, 220 },
            { VendingProduct.ChocolateBar, 300 },
        }.ToImmutableDictionary();
    }

    public IEnumerable<(VendingProduct product, int quantity)> ListStock()
    {
        return new[]
        {
            (VendingProduct.Coke, _stock[VendingProduct.Coke]),
            (VendingProduct.Juice, _stock[VendingProduct.Juice]),
            (VendingProduct.ChocolateBar,
                _stock[VendingProduct.ChocolateBar]),
        };
    }

    public int GetStockFor(VendingProduct product)
    {
        return _stock.ContainsKey(product)
            ? _stock[product]
            : throw new ArgumentOutOfRangeException(
                $"No stock exists for \"{product}\""
            );
    }

    public int GetPriceFor(VendingProduct product)
    {
        if (!_pricing.ContainsKey(product))
            throw new ArgumentOutOfRangeException(
                nameof(product),
                product,
                $"No price found for \"{product}\""
            );

        return _pricing[product];
    }
}