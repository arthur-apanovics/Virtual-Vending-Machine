using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VirtualVendingMachine.Vending;

namespace VirtualVendingMachine.Requests;

public class ListAcceptedCoinsRequest : IRequest<IEnumerable<string>>
{
    public class Handler : IRequestHandler<ListAcceptedCoinsRequest,
        IEnumerable<string>>
    {
        private readonly IVendingDispenser _dispenser;

        public Handler(IVendingDispenser dispenser)
        {
            _dispenser = dispenser;
        }

        public Task<IEnumerable<string>> Handle(
            ListAcceptedCoinsRequest request,
            CancellationToken cancellationToken
        )
        {
            return Task.Run(
                () => _dispenser.AcceptedCoins.Select(c => c.ToString()),
                cancellationToken
            );
        }
    }
}