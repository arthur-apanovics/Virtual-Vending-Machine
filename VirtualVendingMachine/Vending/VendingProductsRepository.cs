using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Vending;

public class VendingProductsRepository : IVendingProductsRepository
{
    private readonly List<Product> _stock = new();
    // TODO: Extract pricing into separate service
    private readonly ImmutableDictionary<Product, int> _pricing;

    public VendingProductsRepository()
    {
        // according to requirements, pricing is not subject to change
        // and is therefore hard-coded
        _pricing = new Dictionary<Product, int>
        {
            { Product.Coke, 180 },
            { Product.Juice, 220 },
            { Product.ChocolateBar, 300 },
        }.ToImmutableDictionary();
    }

    public ImmutableDictionary<Product, int> ListStock()
    {
        return _stock.GroupBy(p => p.Name)
            .ToImmutableDictionary(g => g.First(), g => g.Count());
    }

    public int GetStockFor(Product product)
    {
        return _stock.Count(p => p == product);
    }

    public int GetPriceFor(Product product)
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