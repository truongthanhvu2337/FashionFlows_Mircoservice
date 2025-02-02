using System.Text.Json.Serialization;

namespace FashionFlows.Product.Domain.Entities;

public class Inventory
{
    public int Id { get; set; }
    public Guid ProductId { get; set; }
    public Constant.Size Size { get; set; }
    [JsonIgnore]
    public Product? Product { get; set; }
    public int StockQuantity { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
