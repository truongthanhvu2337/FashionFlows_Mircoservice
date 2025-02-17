using FashionFlows.BuildingBlock.Domain.Events.Interface;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class PaymentFailedEvent : IPaymentFailedEvent
{
    public Guid CorrelationId {  get; set; }
    public List<OrderItemEvent> OrderItemList { get; set; }
}
