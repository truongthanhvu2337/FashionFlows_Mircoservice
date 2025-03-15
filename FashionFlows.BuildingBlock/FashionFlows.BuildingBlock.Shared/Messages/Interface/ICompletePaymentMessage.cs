using FashionFlows.BuildingBlock.Domain.Model;
using MassTransit;

namespace FashionFlows.BuildingBlock.Domain.Messages.Interface;

public interface ICompletePaymentMessage : CorrelatedBy<Guid>
{
    public Guid OrderId { get; set; }
    public string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItem> OrderItemList { get; set; }

}