using System;
using System.Collections.Generic;

namespace VirtualVendingMachine.Vending;

public class ProductsRepository
{
    private readonly Dictionary<VendingProduct, int> _stock;

    public ProductsRepository()
    {
        _stock = new Dictionary<VendingProduct, int>
        {
            { VendingProduct.Coke, 10 },
            { VendingProduct.Juice, 9 },
            { VendingProduct.ChocolateBar, 8 },
        };
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
}