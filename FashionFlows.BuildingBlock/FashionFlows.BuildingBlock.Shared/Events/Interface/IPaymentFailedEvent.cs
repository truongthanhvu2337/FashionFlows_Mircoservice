using MassTransit;

namespace FashionFlows.BuildingBlock.Domain.Events.Interface;

public interface IPaymentFailedEvent : CorrelatedBy<Guid>
{
    public List<OrderItemEvent> OrderItemList { get; set; }
}
