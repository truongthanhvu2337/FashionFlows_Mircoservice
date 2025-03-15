using FashionFlows.BuildingBlock.Domain.Events;
using FashionFlows.BuildingBlock.Domain.Model;

public class StockRollbackMessage
{
    public List<OrderItem> OrderItemList { get; set; }
}