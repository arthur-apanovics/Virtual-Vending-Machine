using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Vending;

public interface IVendingProductsRepository
{
    ImmutableDictionary<Product, int> ListStock();
    int CountStockFor(Product product);
    void AddToStock(IEnumerable<StockItem> products);
    StockItem? TakeFromStock(Product product);
}

public class VendingProductsRepository : IVendingProductsRepository
{
    private readonly List<StockItem> _stock = new();

    public ImmutableDictionary<Product, int> ListStock()
    {
        return _stock.GroupBy(si => si.Product.Name)
            .ToImmutableDictionary(g => g.First().Product, g => g.Count());
    }

    public int CountStockFor(Product product)
    {
        return _stock.Count(si => si.Product == product);
    }

    public void AddToStock(IEnumerable<StockItem> items)
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

    public bool TryFindItemBy(Guid id, out Product? product)
    {
        product = _stock.Find(i => i.Id == id)?.Product;

        return product is not null;
    }
}