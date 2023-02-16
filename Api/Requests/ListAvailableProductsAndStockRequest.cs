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
        IEnumerable<VendingProductDisplayEntryDto>>
{
    public class Handler : IRequestHandler<ListAvailableProductsAndStockRequest,
        IEnumerable<VendingProductDisplayEntryDto>>
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

        public Task<IEnumerable<VendingProductDisplayEntryDto>> Handle(
            ListAvailableProductsAndStockRequest request,
            CancellationToken cancellationToken
        )
        {
            return Task.Run(
                GetListOfAvailableVendingEntries,
                cancellationToken
            );
        }

        private IEnumerable<VendingProductDisplayEntryDto>
            GetListOfAvailableVendingEntries()
        {
            var result = new List<VendingProductDisplayEntryDto>();
            var productStock = _dispenser.ListAvailableProductsAndStock();

            foreach (var (product, qty) in productStock)
            {
                result.Add(
                    new VendingProductDisplayEntryDto(
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