using System;
using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending.Models;

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
        public const int Coke = 180;
        public const int Juice = 220;
        public const int ChocolateBar = 300;

        public static readonly Dictionary<Product, int> DefaultPricing = new()
        {
            { Product.Coke, Coke },
            { Product.Juice, Juice },
            { Product.ChocolateBar, ChocolateBar },
        };
    }

    public static class CoinTill
    {
        public static readonly int[] AcceptedCoinValues =
        {
            10, 20, 50, 100, 200
        };
    }

    public static class VendingDispenser
    {
        public static readonly Coin[] DefaultChangeBank = Array.Empty<Coin>()
            .Concat(Enumerable.Repeat(Coin.Create(10), 30))
            .Concat(Enumerable.Repeat(Coin.Create(15), 20))
            .ToArray();
    }
}