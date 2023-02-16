using System;
using VirtualVendingMachine.Helpers;

namespace VirtualVendingMachine.Tills;

// TODO: Tests
public readonly struct Coin
{
    private Coin(int valueInCents)
    {
        if (valueInCents < 0)
            throw new ArgumentOutOfRangeException(
                nameof(valueInCents),
                "Coin value cannot be negative"
            );

        ValueInCents = valueInCents;
    }

    public int ValueInCents { get; }

    public static Coin Create(int value) => new(value);
    public static Coin Create10() => new(10);
    public static Coin Create20() => new(20);
    public static Coin Create50() => new(50);
    public static Coin Create100() => new(100);
    public static Coin Create200() => new(200);

    public override string ToString() =>
        CurrencyFormatter.CentsAsCurrency(ValueInCents);
}