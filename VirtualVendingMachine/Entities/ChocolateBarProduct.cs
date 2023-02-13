namespace VirtualVendingMachine.Entities;

public record ChocolateBarProduct : VendingProduct
{
    public override string Name => "Chocolate Bar";
    public override int Price => 300;
}