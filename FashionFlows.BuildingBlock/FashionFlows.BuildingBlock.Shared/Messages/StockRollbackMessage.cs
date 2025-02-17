using FashionFlows.BuildingBlock.Domain.Events;

public class StockRollbackMessage
{
    public List<OrderItemEvent> OrderItemList { get; set; }
}