using FashionFlows.BuildingBlock.Domain.Events;
using MassTransit;

namespace FashionFlows.BuildingBlock.Domain.Messages.Interface;

public interface ICompletePaymentMessage : CorrelatedBy<Guid>
{
    public string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItemEvent> OrderItemList { get; set; }

}