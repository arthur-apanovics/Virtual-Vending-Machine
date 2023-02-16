using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Vending;

public class VendingProductsRepository : IVendingProductsRepository
{
    private readonly List<StockItem> _stock = new();

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
        return _stock.GroupBy(si => si.Product.Name)
            .ToImmutableDictionary(g => g.First().Product, g => g.Count());
    }

    public int GetStockFor(Product product)
    {
        return _stock.Count(si => si.Product == product);
    }

    public void AddStock(IEnumerable<StockItem> items)
    {
        _stock.AddRange(items);
    }

    public StockItem? TakeFromStock(Product product)
    {
        var item = _stock.FirstOrDefault(si => si.Product == product);
        if (item is null)
            return null;

        _stock.Remove(item);
        return item;
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

    public bool TryFindItemBy(Guid id, out Product? product)
    {
        product = _stock.Find(i => i.Id == id)?.Product;

        return product is not null;
    }
}