using FashionFlows.BuildingBlock.Domain.Events.Interface;
using FashionFlows.BuildingBlock.Domain.Model;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class OrderFailedEvent : IOrderFailedEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItem> OrderItemList { get; set; }
}
