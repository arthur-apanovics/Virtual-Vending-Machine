namespace VirtualVendingMachine.Dtos;

public record VendingProductDisplayEntryDto(
    string ProductName,
    string ProductPrice,
    int AvailableQuantity
);