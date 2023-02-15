using System.Collections.Generic;

namespace VirtualVendingMachine.Vending;

public interface IVendingProductsRepository
{
    IEnumerable<(VendingProduct product, int quantity)> ListStock();
    int GetStockFor(VendingProduct product);
    int GetPriceFor(VendingProduct product);
}