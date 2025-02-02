namespace FashionFlows.BuildingBlock.Domain.Events;

public class OrderItemEvent
{
    public Guid ProductId { get; set; }
    public int Size { get; set; }
    public int Quantity { get; set; }
}
