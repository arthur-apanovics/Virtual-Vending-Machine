using System.Collections.Generic;

namespace VirtualVendingMachine.Dtos;

public record GetInsertedFundsResponseDto(
    IEnumerable<string> Coins,
    string TotalValue
);