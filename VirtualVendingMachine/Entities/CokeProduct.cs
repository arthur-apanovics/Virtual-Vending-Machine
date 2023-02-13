namespace VirtualVendingMachine.Entities;

public record CokeProduct : VendingProduct
{
    public override string Name => "Coke";
    public override int Price => 180;
}