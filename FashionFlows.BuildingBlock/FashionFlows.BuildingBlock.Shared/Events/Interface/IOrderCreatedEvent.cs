using MassTransit;

namespace FashionFlows.BuildingBlock.Domain.Events.Interface;

public interface IOrderCreatedEvent : CorrelatedBy<Guid>
{
    List<OrderItemEvent> OrderItemList { get; set; }
}
