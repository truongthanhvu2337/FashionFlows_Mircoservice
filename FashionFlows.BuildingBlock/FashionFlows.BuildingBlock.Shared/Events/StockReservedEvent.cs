using FashionFlows.BuildingBlock.Domain.Events.Interface;
using FashionFlows.BuildingBlock.Domain.Model;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class StockReservedEvent : IStockReservedEvent
{
    public Guid CorrelationId { get; set; }
    public List<OrderItem> OrderItemList { get; set; }

}
