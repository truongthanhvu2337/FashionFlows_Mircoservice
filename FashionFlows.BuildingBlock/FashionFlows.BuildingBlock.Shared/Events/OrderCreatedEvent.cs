using FashionFlows.BuildingBlock.Domain.Events.Interface;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class OrderCreatedEvent : IOrderCreatedEvent
{
    public Guid CorrelationId { get; set; }
    public List<OrderItemEvent> OrderItemList { get; set; }
}
