using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VirtualVendingMachine.Dtos;
using VirtualVendingMachine.Vending;

namespace VirtualVendingMachine.Requests;

public class CancelAndRefundRequest : IRequest<RefundResponseDto>
{
    public class
        Handler : IRequestHandler<CancelAndRefundRequest, RefundResponseDto>
    {
        private readonly IVendingDispenser _dispenser;

        public Handler(IVendingDispenser dispenser)
        {
            _dispenser = dispenser;
        }

        public Task<RefundResponseDto> Handle(
            CancelAndRefundRequest request,
            CancellationToken cancellationToken
        )
        {
            return Task.Run(ProcessRefund, cancellationToken);
        }

        private RefundResponseDto ProcessRefund()
        {
            var refundedCoins = _dispenser.CancelAndRefund().ToArray();

            return new RefundResponseDto(
                RefundedCoins: refundedCoins.Select(c => c.ToString()),
                TotalRefundedCoins: refundedCoins.Length
            );
        }
    }
}