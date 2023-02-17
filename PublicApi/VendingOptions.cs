using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Tills.Models;
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

    public static readonly Coin[] CoinTillSupportedCoins = new[]
    {
        Coin.Create10(),
        Coin.Create20(),
        Coin.Create50(),
        Coin.Create100(),
        Coin.Create200(),
    };

    public static readonly Coin[] ChangeBank = Array.Empty<Coin>()
        .Concat(Enumerable.Repeat(Coin.Create10(), 30))
        .Concat(Enumerable.Repeat(Coin.Create20(), 15))
        .ToArray();
}