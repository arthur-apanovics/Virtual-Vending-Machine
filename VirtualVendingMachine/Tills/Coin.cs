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

    public override string ToString() =>
        CurrencyFormatter.CentsAsCurrency(ValueInCents);
}