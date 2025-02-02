namespace FashionFlows.Product.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsAvailable { get; set; } = true;
    public string Brand { get; set; } = string.Empty;

    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
