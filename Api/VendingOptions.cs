using System.Collections.Generic;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine;

public class VendingOptions
{
    public static readonly Dictionary<Product, int> ProductPricing = new()
    {
        { Product.Coke, 180 },
        { Product.Juice, 220 },
        { Product.ChocolateBar, 300 },
    };

    public static readonly Dictionary<Product, int> InitialStock = new()
    {
        { Product.Coke, 10 },
        { Product.Juice, 9 },
        { Product.ChocolateBar, 8 },
    };
}