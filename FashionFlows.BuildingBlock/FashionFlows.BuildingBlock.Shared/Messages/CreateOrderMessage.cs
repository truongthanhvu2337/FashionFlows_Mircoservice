using FashionFlows.BuildingBlock.Domain.Messages.Interface;
using FashionFlows.BuildingBlock.Domain.Model;

namespace FashionFlows.BuildingBlock.Domain.Messages;

public class CreateOrderMessage : ICreateOrderMessage
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }

    public List<OrderItem> OrderItemList { get; set; }
}