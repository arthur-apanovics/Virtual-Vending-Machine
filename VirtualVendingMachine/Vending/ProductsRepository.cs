using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Entities;

namespace VirtualVendingMachine.Vending;

public class ProductsRepository
{
    private readonly List<CokeProduct> _cokeProducts;
    private readonly List<JuiceProduct> _juiceProducts;
    private readonly List<ChocolateBarProduct> _chocolateBarProducts;

    public ProductsRepository()
    {
        _cokeProducts = Enumerable.Repeat(new CokeProduct(), 10).ToList();
        _juiceProducts = Enumerable.Repeat(new JuiceProduct(), 9).ToList();
        _chocolateBarProducts =
            Enumerable.Repeat(new ChocolateBarProduct(), 8).ToList();
    }

    public IEnumerable<(VendingProduct product, int quantity)> ListStock()
    {
        return new (VendingProduct, int)[]
        {
            (new CokeProduct(), _cokeProducts.Count),
            (new JuiceProduct(), _juiceProducts.Count),
            (new ChocolateBarProduct(), _chocolateBarProducts.Count),
        };
    }
}