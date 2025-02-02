namespace FashionFlows.Product.Domain.Model.Reponse;

public class ProductDtoResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<InventoryDtoResponse> Inventories { get; set; } = new();
}

public class InventoryDtoResponse
{
    public int Id { get; set; }
    public Constant.Size Size { get; set; }
    public int StockQuantity { get; set; }
    public DateTime LastUpdated { get; set; }
}

