using FashionFlows.BuildingBlock.Domain.Events.Interface;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class PaymentCompletedEvent : IPaymentCompletedEvent
{
    public Guid CorrelationId {  get; set; }
}
