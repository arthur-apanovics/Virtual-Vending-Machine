using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Vending;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Vending;

namespace VirtualVendingMachineUnitTests.Builders;

public static class VendingProductsRepositoryBuilder
{
    public static VendingProductsRepository Build(
        IEnumerable<StockItem>? stockItems = null
    )
    {
        var productsRepository = new VendingProductsRepository();

        if (stockItems is not null)
            productsRepository.AddStock(stockItems);

        return productsRepository;
    }

    public static VendingProductsRepository BuildWithDefaultStock()
    {
        var productsRepository = new VendingProductsRepository();
        var productQuantities = TestConstants.Stock.DefaultProductQuantities;

        foreach (var (product, qty) in productQuantities)
        {
            productsRepository.AddStock(
                Enumerable.Repeat(StockItem.Create(product), qty)
            );
        }

        return productsRepository;
    }
}