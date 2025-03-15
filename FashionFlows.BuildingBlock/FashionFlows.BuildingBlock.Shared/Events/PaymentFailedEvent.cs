using FashionFlows.BuildingBlock.Domain.Events.Interface;
using FashionFlows.BuildingBlock.Domain.Model;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class PaymentFailedEvent : IPaymentFailedEvent
{
    public Guid CorrelationId {  get; set; }
    public List<OrderItem> OrderItemList { get; set; }
}
