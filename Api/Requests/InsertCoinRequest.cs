using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VirtualVendingMachine.Dtos;
using VirtualVendingMachine.Helpers;
using VirtualVendingMachine.Tills.Models;
using VirtualVendingMachine.Vending;

namespace VirtualVendingMachine.Requests;

public class InsertCoinRequest : IRequest<VendingDispenserStatusDto>
{
    public int CoinValue { get; set; }

    public class Handler : IRequestHandler<InsertCoinRequest,
        VendingDispenserStatusDto>
    {
        private readonly IVendingDispenser _dispenser;

        public Handler(IVendingDispenser dispenser)
        {
            _dispenser = dispenser;
        }

        public Task<VendingDispenserStatusDto> Handle(
            InsertCoinRequest request,
            CancellationToken cancellationToken
        )
        {
            return Task.Run(
                () => InsertCoinAndGetStatusDto(Coin.Create(request.CoinValue)),
                cancellationToken
            );
        }

        private VendingDispenserStatusDto InsertCoinAndGetStatusDto(Coin coin)
        {
            _dispenser.InsertCoin(coin);
            var insertedFundsAmount = CurrencyFormatter.CentsAsCurrency(
                _dispenser.InsertedAmountInCents
            );

            return new VendingDispenserStatusDto(insertedFundsAmount);
        }
    }
}