using FashionFlows.BuildingBlock.Domain.Events;

namespace FashionFlows.BuildingBlock.Domain.Messages.Interface;

public interface ICreateOrderMessage
{

    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }

    public List<OrderItemEvent> OrderItemList { get; set; }
}
