namespace ProductService.Domain.Entities;

public class Product
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public int Stock { get; private set; }

    private Product()
    {
    }

    public Product(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void Update(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }
}
