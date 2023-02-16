namespace VirtualVendingMachine.Vending.Models;

public record StockItem
{
    public Guid Id { get; }
    public Product Product { get; }

    private StockItem(Product product)
    {
        Id = Guid.NewGuid();
        Product = product;
    }

    public static StockItem Create(Product product) => new(product);
}