namespace VirtualVendingMachine.Dtos;

public record VendingProductDisplayEntry(
    string ProductName,
    string ProductPrice,
    int AvailableQuantity
);