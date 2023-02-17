using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Exceptions;

public class ProductOutOfStockException : Exception
{
    public Product ProductOutOfStock { get; }

    public ProductOutOfStockException(Product productOutOfStock)
    {
        ProductOutOfStock = productOutOfStock;
    }
}