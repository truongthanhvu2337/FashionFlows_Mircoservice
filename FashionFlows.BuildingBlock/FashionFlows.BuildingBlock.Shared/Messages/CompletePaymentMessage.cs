using FashionFlows.BuildingBlock.Domain.Events;
using FashionFlows.BuildingBlock.Domain.Messages.Interface;
using FashionFlows.BuildingBlock.Domain.Model;

namespace FashionFlows.BuildingBlock.Domain.Messages;

public class CompletePaymentMessage : ICompletePaymentMessage
{
    public Guid CorrelationId { get; set; }
    public Guid OrderId { get; set; }
    public string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItem> OrderItemList { get; set; }

}