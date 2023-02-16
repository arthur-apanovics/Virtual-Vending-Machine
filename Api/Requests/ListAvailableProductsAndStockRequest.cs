using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VirtualVendingMachine.Dtos;
using VirtualVendingMachine.Helpers;
using VirtualVendingMachine.Vending;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Requests;

public class
    ListAvailableProductsAndStockRequest : IRequest<
        IEnumerable<VendingProductDisplayEntry>>
{
    public class Handler : IRequestHandler<ListAvailableProductsAndStockRequest,
        IEnumerable<VendingProductDisplayEntry>>
    {
        private readonly IVendingDispenser _dispenser;
        private readonly IPricingService _pricingService;

        public Handler(
            IVendingDispenser dispenser,
            IPricingService pricingService
        )
        {
            _dispenser = dispenser;
            _pricingService = pricingService;
        }

        public async Task<IEnumerable<VendingProductDisplayEntry>> Handle(
            ListAvailableProductsAndStockRequest request,
            CancellationToken cancellationToken
        )
        {
            return await Task.Run(
                GetListOfAvailableVendingEntries,
                cancellationToken
            );
        }

        private List<VendingProductDisplayEntry>
            GetListOfAvailableVendingEntries()
        {
            var result = new List<VendingProductDisplayEntry>();
            var productStock = _dispenser.ListAvailableProductsAndStock();

            foreach (var (product, qty) in productStock)
            {
                result.Add(
                    new VendingProductDisplayEntry(
                        product.Name,
                        ProductPrice: GetFormattedProductPrice(product),
                        AvailableQuantity: qty
                    )
                );
            }

            return result;
        }

        private string GetFormattedProductPrice(Product product)
        {
            return CurrencyFormatter.CentsAsCurrency(
                _pricingService.GetPriceFor(product)
            );
        }
    }
}