using System.Collections.Generic;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Dtos;

public record PurchaseProductResponseDto(
    StockItem Item,
    IEnumerable<string> Change
);