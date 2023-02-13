namespace VirtualVendingMachine.Entities;

public record JuiceProduct : VendingProduct
{
    public override string Name => "Juice";
    public override int Price => 220;
}