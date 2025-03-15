using FashionFlows.BuildingBlock.Domain.Events;
using FashionFlows.BuildingBlock.Domain.Model;

namespace FashionFlows.BuildingBlock.Domain.Messages.Interface;

public interface ICreateOrderMessage
{

    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }

    public List<OrderItem> OrderItemList { get; set; }
}
