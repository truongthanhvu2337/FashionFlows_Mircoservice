namespace FashionFlows.BuildingBlock.Domain.Model;

public class OrderItem
{
    public Guid ProductId { get; set; }
    public int Size { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
