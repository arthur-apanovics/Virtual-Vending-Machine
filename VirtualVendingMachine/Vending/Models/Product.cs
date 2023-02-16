namespace VirtualVendingMachine.Vending.Models;

// TODO: TESTS
public record Product
{
    public string Name { get; }

    private Product(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;

    public static Product Coke => new("Coke");

    public static Product Juice => new("Juice");

    public static Product ChocolateBar => new("Chocolate Bar");

    // TODO: Somehow enforce new products to be added to this list
    public static IEnumerable<Product> AllProducts =>
        new[] { Coke, Juice, ChocolateBar };
}