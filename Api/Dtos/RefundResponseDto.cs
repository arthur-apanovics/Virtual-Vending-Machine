using System.Collections.Generic;

namespace VirtualVendingMachine.Dtos;

public record RefundResponseDto(
    IEnumerable<string> RefundedCoins,
    int TotalRefundedCoins
);