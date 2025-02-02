using System.Text.Json.Serialization;

namespace FashionFlows.Order.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Size { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    [JsonIgnore]
    public Order Order { get; set; }
}
