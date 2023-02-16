using System.Collections.Generic;
using VirtualVendingMachine.Vending;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachineUnitTests.Vending;

namespace VirtualVendingMachineUnitTests.Builders;

public static class PricingServiceBuilder
{
    public static PricingService Build(
        Dictionary<Product, int>? withPricing = null
    ) =>
        new(withPricing ?? TestConstants.Pricing.DefaultPricing);
}