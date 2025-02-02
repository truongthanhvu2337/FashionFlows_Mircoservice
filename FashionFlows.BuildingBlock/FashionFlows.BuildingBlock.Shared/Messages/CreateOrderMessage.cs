using FashionFlows.BuildingBlock.Domain.Events;
using FashionFlows.BuildingBlock.Domain.Messages.Interface;

namespace FashionFlows.BuildingBlock.Domain.Messages;

public class CreateOrderMessage : ICreateOrderMessage
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }

    public List<OrderItemEvent> OrderItemList { get; set; }
}