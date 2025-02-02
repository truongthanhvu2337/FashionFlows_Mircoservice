using FashionFlows.BuildingBlock.Domain.Events.Interface;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class OrderFailedEvent : IOrderFailedEvent
{
    public Guid OrderId { get; set; }
    //public string CustomerId { get; set; }
}
