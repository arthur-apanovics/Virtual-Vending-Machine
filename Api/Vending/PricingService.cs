using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine.Vending;

public interface IPricingService
{
    int GetPriceFor(Product product);
}

public class PricingService : IPricingService
{
    private readonly ImmutableDictionary<Product, int> _pricing;

    public PricingService(Dictionary<Product, int> pricing)
    {
        _pricing = pricing.ToImmutableDictionary();
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