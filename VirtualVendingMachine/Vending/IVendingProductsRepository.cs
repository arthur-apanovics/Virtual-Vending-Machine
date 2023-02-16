using System.Collections.Generic;
using System.Collections.Immutable;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Vending;

public interface IVendingProductsRepository
{
    ImmutableDictionary<Product, int> ListStock();
    int GetStockFor(Product product);
    void AddStock(IEnumerable<StockItem> products);
    int GetPriceFor(Product product);
}