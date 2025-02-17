using FashionFlows.BuildingBlock.Domain.Events;
using FashionFlows.BuildingBlock.Domain.Messages.Interface;

namespace FashionFlows.BuildingBlock.Domain.Messages;

public class CompletePaymentMessage : ICompletePaymentMessage
{
    public Guid CorrelationId { get; set; }
    public string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItemEvent> OrderItemList { get; set; }

}