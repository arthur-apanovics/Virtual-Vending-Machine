namespace VirtualVendingMachine.Tills;

public struct Coin
{
    public Coin(int valueInCents)
    {
        ValueInCents = valueInCents;
    }

    public int ValueInCents { get; }
}