using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VirtualVendingMachine.Dtos;
using VirtualVendingMachine.Vending;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Requests;

public class PurchaseProductRequest : IRequest<PurchaseProductResponseDto>
{
    public string ProductName { get; set; } = null!;

    public class Handler : IRequestHandler<PurchaseProductRequest,
        PurchaseProductResponseDto>
    {
        private readonly IVendingDispenser _dispenser;

        public Handler(IVendingDispenser dispenser)
        {
            _dispenser = dispenser;
        }

        public Task<PurchaseProductResponseDto> Handle(
            PurchaseProductRequest request,
            CancellationToken cancellationToken
        )
        {
            return Task.Run(() => ProcessPurchase(request), cancellationToken);
        }

        private PurchaseProductResponseDto ProcessPurchase(
            PurchaseProductRequest request
        )
        {
            var (item, change) =
                _dispenser.Dispense(Product.Create(request.ProductName));

            return new PurchaseProductResponseDto(
                item,
                change.Select(c => c.ToString())
            );
        }
    }
}