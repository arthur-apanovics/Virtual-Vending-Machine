using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Exceptions;

public class NotKnownProductException : Exception
{
    public Product UnknownProduct { get; }

    public NotKnownProductException(Product unknownProduct)
    {
        UnknownProduct = unknownProduct;
    }
}