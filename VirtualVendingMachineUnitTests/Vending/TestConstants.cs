using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachine.Tills.Models;

namespace VirtualVendingMachineUnitTests.Vending;

public static class TestConstants
{
    public static class Stock
    {
        public static readonly Dictionary<Product, int>
            DefaultProductQuantities = new()
            {
                { Product.Coke, 10 },
                { Product.Juice, 9 },
                { Product.ChocolateBar, 8 },
            };
    }

    public static class Pricing
    {
        public static readonly Dictionary<Product, int> DefaultPricing = new()
        {
            { Product.Coke, 180 },
            { Product.Juice, 220 },
            { Product.ChocolateBar, 300 },
        };
    }

    public static class CoinTill
    {
        public static readonly Coin[] SupportedCoins =
        {
            Coin.Create10(),
            Coin.Create20(),
            Coin.Create50(),
            Coin.Create100(),
            Coin.Create200(),
        };
    }

    public static class VendingDispenser
    {
        public static readonly Coin[] DefaultChangeBank = Array.Empty<Coin>()
            .Concat(Enumerable.Repeat(Coin.Create10(), 30))
            .Concat(Enumerable.Repeat(Coin.Create20(), 15))
            .ToArray();

        public static readonly int[] DefaultAcceptedCoinValues =
        {
            10, 20, 50, 100, 200
        };
    }
}