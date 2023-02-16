using System;
using System.Collections.Generic;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Vending;

public interface IPricingService
{
    int GetPriceFor(Product product);
}

public class PricingService : IPricingService
{
    private readonly Dictionary<Product, int> _pricing;

    public PricingService(Dictionary<Product, int> pricing)
    {
        _pricing = pricing;
    }

    public int GetPriceFor(Product product)
    {
        if (!_pricing.ContainsKey(product))
            throw new ArgumentOutOfRangeException(
                nameof(product),
                product,
                $"No price found for \"{product}\""
            );

        return _pricing[product];
    }
}