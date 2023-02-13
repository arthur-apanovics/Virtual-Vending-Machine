namespace VirtualVendingMachine.Entities;

public abstract record VendingProduct
{
    /// <summary>
    /// Product name
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Price in cents
    /// </summary>
    public abstract int Price { get; }
}