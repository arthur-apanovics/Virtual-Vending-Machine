using System;
using System.Linq;
using VirtualVendingMachine.Tills;

namespace VirtualVendingMachineUnitTests.Vending;

public static class TestConstants
{
    public static class Stock
    {
        public const int CokeDefaultStockQty = 10;
        public const int JuiceDefaultStockQty = 9;
        public const int ChocolateBarDefaultStockQty = 8;
    }

    public static class Pricing
    {
        public const int Coke = 180;
        public const int Juice = 220;
        public const int ChocolateBar = 300;
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