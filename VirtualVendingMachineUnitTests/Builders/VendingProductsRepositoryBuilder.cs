using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Vending;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Vending;

namespace VirtualVendingMachineUnitTests.Builders;

public static class VendingProductsRepositoryBuilder
{
    public static VendingProductsRepository Build(
        IEnumerable<StockItem>? withStockItems = null
    )
    {
        var productsRepository = new VendingProductsRepository();

        if (withStockItems is not null)
            productsRepository.AddToStock(withStockItems);

        return productsRepository;
    }

    public static VendingProductsRepository BuildWithDefaultStock()
    {
        var productsRepository = new VendingProductsRepository();
        var productQuantities = TestConstants.Stock.DefaultProductQuantities;

        foreach (var (product, qty) in productQuantities)
        {
            productsRepository.AddToStock(
                Enumerable.Repeat(StockItem.Create(product), qty)
            );
        }

        return productsRepository;
    }
}