using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VirtualVendingMachine.Dtos;
using VirtualVendingMachine.Helpers;
using VirtualVendingMachine.Vending;

namespace VirtualVendingMachine.Requests;

public class GetInsertedFundsRequest : IRequest<GetInsertedFundsResponseDto>
{
    public class Handler : IRequestHandler<GetInsertedFundsRequest,
        GetInsertedFundsResponseDto>
    {
        private readonly IVendingDispenser _dispenser;

        public Handler(IVendingDispenser dispenser)
        {
            _dispenser = dispenser;
        }

        public Task<GetInsertedFundsResponseDto> Handle(
            GetInsertedFundsRequest request,
            CancellationToken cancellationToken
        )
        {
            return Task.Run(
                () => new GetInsertedFundsResponseDto(
                    Coins: _dispenser.InsertedCoins.Select(c => c.ToString()),
                    TotalValue: CurrencyFormatter.CentsAsCurrency(
                        _dispenser.InsertedAmountInCents
                    )
                ),
                cancellationToken
            );
        }
    }
}