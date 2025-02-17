using FashionFlows.BuildingBlock.Domain.Events.Interface;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class OrderCompletedEvent : IOrderCompletedEvent
{
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
}
