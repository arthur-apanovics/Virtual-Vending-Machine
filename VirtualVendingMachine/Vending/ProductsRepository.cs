using System;
using System.Collections.Generic;
using VirtualVendingMachine.Entities;

namespace VirtualVendingMachine.Vending;

public class ProductsRepository
{
    private readonly Dictionary<Type, int> _stock;

    public ProductsRepository()
    {
        _stock = new Dictionary<Type, int>
        {
            {typeof(CokeProduct), 10},
            {typeof(JuiceProduct), 9},
            {typeof(ChocolateBarProduct), 8},
        };
    }

    public IEnumerable<(VendingProduct product, int quantity)> ListStock()
    {
        return new (VendingProduct, int)[]
        {
            (new CokeProduct(), _stock[typeof(CokeProduct)]),
            (new JuiceProduct(), _stock[typeof(JuiceProduct)]),
            (new ChocolateBarProduct(), _stock[typeof(ChocolateBarProduct)]),
        };
    }
}