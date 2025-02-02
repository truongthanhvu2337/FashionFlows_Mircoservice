using FashionFlows.BuildingBlock.Domain.Events.Interface;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class StockReservedEvent : IStockReservedEvent
{
    public Guid CorrelationId { get; set; }
    public List<OrderItemEvent> OrderItemList { get; set; }

}
